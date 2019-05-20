using System;
using analyst_challenge.Controllers;
using analyst_challenge.Models;
using NUnit.Framework;

namespace analyst_challenge_test.Controllers
{
    [TestFixture]
    public class EventReceiverControllerTest
    {
        private readonly EventReceiverController _eventReceiverController;

        public EventReceiverControllerTest(EventReceiverController eventReceiverController)
        {
            _eventReceiverController = eventReceiverController;
        }
    
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
        
        [Test]
        public void TestCreateEvent()
        {
            var result = _eventReceiverController.Create(new EventReceiver
            {
                Timestamp = DateTime.Now,
                Valor = "teste",
                Tag = "brasil.sudeste.sensor01"
            });
            
            Assert.Pass();
        }
    }
}
