using DomainInterfaces;

namespace Infrastructure
{
    public class TextProvider : ITextProvider
    {
        public string FetchText()
        {
            return "Hello world!";
        }
    }
}
