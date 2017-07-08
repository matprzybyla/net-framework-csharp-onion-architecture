using System;

namespace Cfw.Domain
{
    public class Message : IMessage
    {
        public string Text { get; set; }

        public Guid Id { get; set; }
    }
}
