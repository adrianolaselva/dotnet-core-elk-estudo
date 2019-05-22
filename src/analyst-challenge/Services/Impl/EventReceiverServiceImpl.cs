using System;
using System.Collections.Generic;
using Amazon.SQS;
using Amazon.SQS.Model;
using analyst_challenge.DAO;
using analyst_challenge.Enums;
using analyst_challenge.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nest;
using Newtonsoft.Json;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace analyst_challenge.Services.Impl
{
    public class EventReceiverServiceImpl : IEventReceiverService
    {
        private readonly IConfiguration _configuration;
        private readonly IEventReceiverDAO _receiverDao;
        private readonly ILogger<EventReceiverServiceImpl> _logger;
        private readonly SendMessageRequest _sendMessageRequest;
        private readonly IAmazonSQS _sqs;

        public EventReceiverServiceImpl(ILogger<EventReceiverServiceImpl> logger, IAmazonSQS sqs,
            IConfiguration configuration, 
            IEventReceiverDAO receiverDao)
        {
            _logger = logger;
            _sqs = sqs;
            _configuration = configuration;
            _receiverDao = receiverDao;
            _sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = configuration["AWS_SQS_QUEUE_URL_EVENT_RECEIVER"]
            };
        }

        public EventReceiver FindById(string uuid)
        {
            return _receiverDao.FindById(uuid);
        }

        public IReadOnlyCollection<EventReceiver> List(int from, int size, string tag)
        {

            var search = new SearchDescriptor<EventReceiver>();

            search
                .From(from)
                .Size(size)
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.Tag)
                        .Query(tag)
                    )
                );

            return _receiverDao.List(search);
        }

        /// <summary>
        ///     Método responsável por criar evento e adicionar na fila para persistir de forma assíncrona
        /// </summary>
        /// <param name="eventReceiver"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public EventReceiver Create(EventReceiver eventReceiver)
        {
            try
            {
                eventReceiver.Id = Guid.NewGuid();
                eventReceiver.Status = EventStatus.PROCESSED;

                if (eventReceiver.Valor == null || eventReceiver.Valor.Trim() == "")
                    eventReceiver.Status = EventStatus.ERROR;

                _sendMessageRequest.MessageBody = JsonConvert.SerializeObject(eventReceiver);
                _sqs.SendMessageAsync(_sendMessageRequest);
                _logger.Log(LogLevel.Information,
                    $"Evento regitrado com sucesso => [{JsonConvert.SerializeObject(eventReceiver)}]");
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, exception.StackTrace);
                throw new Exception(
                    $"Falha ao registrar evento => [{JsonConvert.SerializeObject(eventReceiver)}, {exception.Message}]");
            }

            return eventReceiver;
        }
        
        
    }
}
