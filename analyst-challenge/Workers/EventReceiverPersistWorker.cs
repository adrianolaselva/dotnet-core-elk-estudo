using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using analyst_challenge.DAO;
using analyst_challenge.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace analyst_challenge.Workers
{
    public class EventReceiverPersistWorker : IHostedService, IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EventReceiverPersistWorker> _logger;
        private readonly ReceiveMessageRequest _receiveMessageRequest;
        private readonly IEventReceiverDAO _receiverDao;
        private readonly IAmazonSQS _sqs;

        public EventReceiverPersistWorker(
            ILogger<EventReceiverPersistWorker> logger,
            IAmazonSQS sqs,
            IConfiguration configuration,
            IEventReceiverDAO receiverDao)
        {
            _logger = logger;
            _sqs = sqs;
            _configuration = configuration;
            _receiverDao = receiverDao;

            _receiveMessageRequest = new ReceiveMessageRequest
            {
                QueueUrl = configuration["AWS_SQS_QUEUE_URL_EVENT_RECEIVER"],
                WaitTimeSeconds = 20
            };
        }

        public void Dispose()
        {
            _sqs.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.Log(LogLevel.Information, "Início processamento");

            do
            {
                var receiveMessageResponse = _sqs.ReceiveMessageAsync(_receiveMessageRequest).Result;

                _logger.Log(LogLevel.Information,
                    $"Foram encontradas {receiveMessageResponse.Messages.Count} mensagens para serem processadas");

                foreach (var message in receiveMessageResponse.Messages)
                {
                    _logger.Log(LogLevel.Information, $"Mensagem recebida para processamento [{message.MessageId}]");
                    _logger.Log(LogLevel.Information, $"Handle [{message.ReceiptHandle}]");
                    _logger.Log(LogLevel.Information, $"Conteúdo [{message.Body}]");

                    var eventReceiver = JsonConvert.DeserializeObject<EventReceiver>(message.Body);

                    _logger.Log(LogLevel.Information, eventReceiver.ToString());

                    _receiverDao.Create(JsonConvert.DeserializeObject<EventReceiver>(message.Body));

                    var messageReceiptHandle = receiveMessageResponse.Messages.FirstOrDefault()?.ReceiptHandle;

                    _sqs.DeleteMessageAsync(new DeleteMessageRequest
                    {
                        QueueUrl = _configuration["AWS_SQS_QUEUE_URL_EVENT_RECEIVER"],
                        ReceiptHandle = message.ReceiptHandle
                    }, cancellationToken);
                }

                Task.Delay(5, cancellationToken);
            } while (!cancellationToken.IsCancellationRequested);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.Log(LogLevel.Information, "Fim processamento");

            return Task.CompletedTask;
        }
    }
}
