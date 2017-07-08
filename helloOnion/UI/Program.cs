using Domain;
using Infrastructure;
using Service;
using ServiceInterfaces;
using System;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleMessage msg = PrepareMessage();

            Console.WriteLine(msg.Text);
            Console.ReadLine();
        }

        private static SimpleMessage PrepareMessage()
        {
            IMessagingService service = new MessagingService(new TextProvider());
            return service.GetNextMessage();
        }
    }
}
