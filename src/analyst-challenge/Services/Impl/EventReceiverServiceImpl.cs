using System;
using Amazon.SQS;
using Amazon.SQS.Model;
using analyst_challenge.Enums;
using analyst_challenge.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace analyst_challenge.Services.Impl
{
    public class EventReceiverServiceImpl : IEventReceiverService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EventReceiverServiceImpl> _logger;
        private readonly SendMessageRequest _sendMessageRequest;
        private readonly IAmazonSQS _sqs;

        public EventReceiverServiceImpl(ILogger<EventReceiverServiceImpl> logger, IAmazonSQS sqs,
            IConfiguration configuration)
        {
            _logger = logger;
            _sqs = sqs;
            _configuration = configuration;
            _sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = configuration["AWS_SQS_QUEUE_URL_EVENT_RECEIVER"]
            };
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
