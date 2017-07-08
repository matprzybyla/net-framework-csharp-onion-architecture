using Cfw.ServiceInterfaces;
using System;
using System.Linq;
using Cfw.Domain;
using System.Xml.Linq;
using System.IO;

namespace Cfw.Infrastructure
{
    public class XmlFileRepository : IMessageRepository
    {

        #region Private Fields

        private const string DEFAULT_FILE_NAME = "repository.xml";

        private readonly string _fileName;

        private bool _isInitialized;

        #endregion Private Fields

        #region Public Constructors

        public XmlFileRepository(string fileName = "")
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

            bool needNew = true;
            if (File.Exists(_fileName))
            {
                needNew = TryAppendToFile(token);
            }

            if (needNew)
            {
                FillNewFile(token);
            }            

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

            if (message.Text == null)
            {
                return;
            }

            XDocument xdoc = XDocument.Load(_fileName);
            XElement tokenElement = xdoc.Descendants("token").LastOrDefault();
            tokenElement.Add(
                new XElement("message",
                    new XAttribute("id", message.Id),
                    new XText(message.Text)
                )
            );

            xdoc.Save(_fileName);
        }

        #endregion Public Methods

        #region Private Methods

        private void FillNewFile(IToken token)
        {
            XDocument xdoc = new XDocument(
                    new XElement("doc",
                        new XElement("token",
                            new XAttribute("timestamp", token.Timestamp.ToString()),
                            new XAttribute("userName", token.UserName)
                        )
                    )
                );

            xdoc.Save(_fileName);
        }

        private bool TryAppendToFile(IToken token)
        {
            try
            {
                XDocument xdoc = XDocument.Load(_fileName);
                xdoc.Root.Add(
                    new XElement("token",
                        new XAttribute("timestamp", token.Timestamp.ToString()),
                        new XAttribute("userName", token.UserName)
                    )
                );

                xdoc.Save(_fileName);
            }
            catch (Exception)
            {
                return true;
            }

            return false;
        }

        #endregion Private Methods

    }
}
