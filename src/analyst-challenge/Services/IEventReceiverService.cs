using analyst_challenge.Models;

namespace analyst_challenge.Services
{
    public interface IEventReceiverService
    {
        EventReceiver Create(EventReceiver eventReceiver);
    }
}
