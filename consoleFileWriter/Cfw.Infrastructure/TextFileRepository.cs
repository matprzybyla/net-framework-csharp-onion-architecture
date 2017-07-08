using Cfw.Domain;
using Cfw.ServiceInterfaces;
using System;
using System.IO;

namespace Cfw.Infrastructure
{
    public class TextFileRepository : IMessageRepository
    {

        #region Private Fields

        private const string DEFAULT_FILE_NAME = "repository.txt";

        private readonly string _fileName;

        private bool _isInitialized;

        #endregion Private Fields

        #region Public Constructors

        public TextFileRepository(string fileName = "")
        {
            _fileName = string.IsNullOrEmpty(fileName) ? DEFAULT_FILE_NAME : fileName;
        }

        #endregion Public Constructors

        #region Public Properties

        public bool IsInitialized => _isInitialized;

        #endregion Public Properties

        #region Public Methods

        public void Initialize(IToken token)
        {
            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            File.AppendAllLines(_fileName, new string[] { string.Format("{0}\t{1} typed:", token.Timestamp, token.UserName) });

            _isInitialized = true;
        }

        public void WriteMessage(IMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (!_isInitialized)
            {
                throw new InvalidOperationException("Cannot write message to uninitialized repository!");
            }

            if (message.Text != null)
            {
                File.AppendAllLines(_fileName, new string[] { string.Format("{0}\t{1}", message.Id, message.Text) });
            }
        }

        #endregion Public Methods

    }
}
