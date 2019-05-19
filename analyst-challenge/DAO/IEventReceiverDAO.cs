using analyst_challenge.Models;

namespace analyst_challenge.DAO
{
    public interface IEventReceiverDAO
    {
        EventReceiver FindById(EventReceiver eventReceiver);
        
        EventReceiver List(EventReceiver eventReceiver);
        
        EventReceiver Create(EventReceiver eventReceiver);
        
    }
}
