using Cfw.ServiceInterfaces;
using System;
using Cfw.Domain;

namespace Cfw.Services
{
    public class TokenManager : ITokenManager
    {
        #region Public Methods

        public IToken CreateNew(string userName)
        {
            if (userName == null)
            {
                throw new ArgumentNullException(nameof(userName));
            }

            return new Token
            {
                UserName = string.IsNullOrEmpty(userName) ? "Nobody" : userName,
                Timestamp = DateTime.UtcNow
            };
        }

        public bool IsValid(IToken token)
        {
            if (token?.UserName == null)
            {
                return false;
            }

            return token.Timestamp <= DateTime.UtcNow;
        }

        #endregion Public Methods
    }
}
