using System.Collections.Generic;
using analyst_challenge.Models;
using Nest;

namespace analyst_challenge.DAO
{
    public interface IEventReceiverDAO
    {
        EventReceiver FindById(string uuid);

        IReadOnlyCollection<EventReceiver> List(SearchDescriptor<EventReceiver> search);

        EventReceiver Create(EventReceiver eventReceiver);
        
    }
}
