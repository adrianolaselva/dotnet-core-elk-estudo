using analyst_challenge.Models;
using Nest;

namespace analyst_challenge.DAO.Impl
{
    public class EventReceiverDAOImpl : IEventReceiverDAO
    {
        private readonly IElasticClient _elasticClient;

        public EventReceiverDAOImpl(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public EventReceiver FindById(EventReceiver eventReceiver)
        {
            _elasticClient.IndexDocument(eventReceiver);

            return eventReceiver;
        }

        public EventReceiver List(EventReceiver eventReceiver)
        {
            _elasticClient.Search<EventReceiver>();

            return eventReceiver;
        }

        public EventReceiver Create(EventReceiver eventReceiver)
        {
            _elasticClient.IndexDocument(eventReceiver);

            return eventReceiver;
        }
    }
}
