using System.Collections.Generic;
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

        public EventReceiver FindById(string uuid)
        {
            var eventReceiver = _elasticClient.Get<EventReceiver>(uuid);

            return eventReceiver.Source;
        }

        public IReadOnlyCollection<EventReceiver> List(SearchDescriptor<EventReceiver> search)
        {
            
            var response = _elasticClient.Search<EventReceiver>(s => search);
            
            return response.Documents;
        }

        public EventReceiver Create(EventReceiver eventReceiver)
        {
            _elasticClient.IndexDocument(eventReceiver);

            return eventReceiver;
        }
    }
}
