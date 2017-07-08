using Cfw.ServiceInterfaces;
using System;
using Cfw.Domain;

namespace Cfw.Services
{
    public class MessageService : IMessageService
    {

        #region Private Fields

        IMessageRepository _messageRepository;
        ITokenManager _tokenManager;

        #endregion Private Fields

        #region Public Constructors

        public MessageService(ITokenManager tokenManager, IMessageRepository messageRepository)
        {
            _tokenManager = tokenManager;
            _messageRepository = messageRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public bool WriteMessage(IToken userToken, IMessage messageToWrite)
        {
            if (userToken == null || messageToWrite == null)
            {
                return false;
            }

            if (!_tokenManager.IsValid(userToken))
            {
                return false;
            }

            if (!CheckMessage(messageToWrite))
            {
                return false;
            }

            try
            {
                if (!_messageRepository.IsInitialized)
                {
                    _messageRepository.Initialize(userToken);
                }

                _messageRepository.WriteMessage(messageToWrite);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }

            return true;
        }

        #endregion Public Methods

        #region Private Methods

        private static bool CheckMessage(IMessage message)
        {
            return message != null && message.Id != Guid.Empty && message.Text != null;
        }

        #endregion Private Methods

    }
}
