using Domain;

namespace ServiceInterfaces
{
    public interface IMessagingService
    {
        SimpleMessage GetNextMessage();
    }
}
