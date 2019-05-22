using System.Collections.Generic;
using analyst_challenge.Enums;
using analyst_challenge.Models;
using Nest;

namespace analyst_challenge.Services
{
    public interface IEventReceiverService
    {
        EventReceiver FindById(string uuid);

        IReadOnlyCollection<EventReceiver> List(int from, int size, string tag);
        
        EventReceiver Create(EventReceiver eventReceiver);
    }
}
