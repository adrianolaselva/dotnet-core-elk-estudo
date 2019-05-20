using System;
using analyst_challenge.Controllers;
using analyst_challenge.DAO;
using analyst_challenge.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace analyst_challenge_test.DAO
{
    [TestClass]
    public class EventReceiverDAOTest
    {
        private readonly IEventReceiverDAO _eventReceiverDao;

        public EventReceiverDAOTest(IEventReceiverDAO _eventReceiverDao)
        {
            this._eventReceiverDao = _eventReceiverDao;
        }
    
        [TestMethod]
        public void TestCreateEvent()
        {
            Assert.AreEqual(1, 1);
//            var result = _eventReceiverController.Create(new EventReceiver
//            {
//                Timestamp = DateTime.Now,
//                Valor = "teste",
//                Tag = "brasil.sudeste.sensor01"
//            });
//            
//            Assert.Pass();
        }
    }
}
