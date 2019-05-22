using System;
using analyst_challenge.DAO;
using analyst_challenge.Models;
using analyst_challenge_test.Helpers;
using Nest;
using Xunit;

namespace analyst_challenge.tests.DAO
{
    public class EventReceiverDAOTest
    {
        private readonly IEventReceiverDAO _eventReceiverDao;

        public EventReceiverDAOTest(IEventReceiverDAO eventReceiverDao)
        {
            _eventReceiverDao = eventReceiverDao;
        }

        [Fact]
        public void EventCreate_Test()
        {
            var receiver = _eventReceiverDao.Create(new EventReceiver
            {
                Timestamp = new DateTime(1539112021),
                Tag = "brasil.sudeste.sensor01",
                Valor = "912391232193912"
            });
            
            Assert.Equal("brasil.sudeste.sensor01", receiver.Tag);
        }
        
        [Fact]
        public void EventFindById_Test()
        {
            var receiver = _eventReceiverDao.FindById("36435b28-250b-40c2-85a5-1ce657cfe359");
            
            Assert.Null(receiver);
        }
        
        [Fact]
        public void EventList_Test()
        {
            var search = new SearchDescriptor<EventReceiver>();

            search
                .From(0)
                .Size(10);
            
            var receiver = _eventReceiverDao.List(search);
            
            Assert.Equal(0, receiver.Count);
        }
        
    }
}
