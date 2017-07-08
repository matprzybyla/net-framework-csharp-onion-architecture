using System;

namespace Cfw.Domain
{
    public class Token : IToken
    {
        public string UserName { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
