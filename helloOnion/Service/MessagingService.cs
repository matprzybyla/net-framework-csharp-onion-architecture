using ServiceInterfaces;
using Domain;
using DomainInterfaces;

namespace Service
{
    public class MessagingService : IMessagingService
    {
        ITextProvider _provider;

        public MessagingService(ITextProvider provider)
        {
            _provider = provider;
        }

        public SimpleMessage GetNextMessage()
        {
            return new SimpleMessage { Text = _provider.FetchText() };
        }
    }
}
