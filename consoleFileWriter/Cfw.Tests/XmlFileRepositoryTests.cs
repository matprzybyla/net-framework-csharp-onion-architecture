using Cfw.Infrastructure;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Cfw.Tests
{
    /// <summary>
    /// Tests XmlFileRepository with edge cases, proper initialization order and expected file content for given IToken and IMessage.
    /// The tests check if created XML structure is correct and can be parsed using standard XDocument mechanisms.
    /// Before each test a new temp file is created to be used in test. It is deleted after each test from temp folder.
    /// </summary>
    [TestFixture]
    public class XmlFileRepositoryTests
    {
        #region Private Fields

        string _fileName;
        XmlFileRepository _repository;

        #endregion Private Fields

        #region Public Methods

        [TearDown]
        public void Deinitialize()
        {
            if (File.Exists(_fileName))
            {
                System.Diagnostics.Debug.Write(File.ReadAllText(_fileName));

                File.Delete(_fileName);
            }
        }

        [SetUp]
        public void Initalize()
        {
            _fileName = Path.GetTempFileName();
            _repository = new XmlFileRepository(_fileName);
        }

        [Test]
        public void XmlFileRepositoryInvalidMessageTest()
        {
            var message = TestHelpers.FakeInvalidMessage();
            var token = TestHelpers.FakeToken();

            _repository.Initialize(token);
            _repository.WriteMessage(message);

            XDocument xdoc = XDocument.Load(_fileName);
            XElement tokenElement = xdoc.Document.Descendants("token").FirstOrDefault();
            Assert.IsNotNull(tokenElement);
            Assert.IsFalse(tokenElement.Nodes().Any());
        }

        /// <summary>
        /// We expect InvalidOperationException when trying to write data into repository that was not initalized before with user token.
        /// </summary>
        [Test]
        public void XmlFileRepositoryNotInitializedWriteTest()
        {
            var message = TestHelpers.FakeMessage();
            Assert.Throws<InvalidOperationException>(() => _repository.WriteMessage(message));
        }

        [Test]
        public void XmlFileRepositoryNullInitializationTest()
        {
            Assert.Throws<ArgumentNullException>(() => _repository.Initialize(null));
        }

        [Test]
        public void XmlFileRepositoryNullMessageTest()
        {
            Assert.Throws<ArgumentNullException>(() => _repository.WriteMessage(null));
        }

        [Test]
        public void XmlFileRepositoryProperInitializationTest()
        {
            var token = TestHelpers.FakeToken();

            _repository.Initialize(token);

            XDocument xdoc = XDocument.Load(_fileName);
            XElement tokenElement = xdoc.Document.Descendants("token").FirstOrDefault();
            Assert.IsNotNull(tokenElement);
            Assert.AreEqual(string.Format("<token timestamp=\"{0}\" userName=\"{1}\" />", token.Timestamp, token.UserName), tokenElement.ToString());
        }

        [Test]
        public void XmlFileRepositoryProperMessagesTest()
        {
            var message = TestHelpers.FakeMessage();
            var token = TestHelpers.FakeToken();

            _repository.Initialize(token);
            _repository.WriteMessage(message);

            XDocument xdoc = XDocument.Load(_fileName);
            XElement messageElement = xdoc.Document.Descendants("message").FirstOrDefault();
            Assert.IsNotNull(messageElement);
            Assert.AreEqual(string.Format("<message id=\"{0}\">{1}</message>", message.Id, message.Text), messageElement.ToString());
        }

        #endregion Public Methods
    }
}
