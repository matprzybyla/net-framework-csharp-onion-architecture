using Cfw.Infrastructure;
using NUnit.Framework;
using System;
using System.IO;

namespace Cfw.Tests
{
    /// <summary>
    /// Tests TextFileRepository with edge cases, proper initialization order and expected file content for given IToken and IMessage. 
    /// Before each test a new temp file is created to be used in test. It is deleted after each test from temp folder.
    /// </summary>
    [TestFixture]
    public class TextFileRepositoryTests
    {

        #region Private Fields

        string _fileName;
        TextFileRepository _repository;

        #endregion Private Fields

        #region Public Methods

        [TearDown]
        public void Deinitialize()
        {
            if (File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }
        }

        [SetUp]
        public void Initalize()
        {
            _fileName = Path.GetTempFileName();
            _repository = new TextFileRepository(_fileName);
        }

        [Test]
        public void TextFileRepositoryInvalidMessageTest()
        {
            var message = TestHelpers.FakeInvalidMessage();
            var token = TestHelpers.FakeToken();

            _repository.Initialize(token);
            _repository.WriteMessage(message);

            string[] fileContents = File.ReadAllLines(_fileName);
            Assert.AreEqual(1, fileContents.Length);
        }

        /// <summary>
        /// We expect InvalidOperationException when trying to write data into repository that was not initalized before with user token.
        /// </summary>
        [Test]
        public void TextFileRepositoryNotInitializedWriteTest()
        {
            var message = TestHelpers.FakeMessage();
            Assert.Throws<InvalidOperationException>(() => _repository.WriteMessage(message));
        }

        [Test]
        public void TextFileRepositoryNullInitializationTest()
        {
            Assert.Throws<ArgumentNullException>(() => _repository.Initialize(null));
        }

        [Test]
        public void TextFileRepositoryNullMessageTest()
        {
            Assert.Throws<ArgumentNullException>(() => _repository.WriteMessage(null));
        }

        [Test]
        public void TextFileRepositoryProperInitializationTest()
        {
            var token = TestHelpers.FakeToken();

            _repository.Initialize(token);

            string[] fileContents = File.ReadAllLines(_fileName);
            Assert.AreEqual(fileContents.Length, 1);
            Assert.AreEqual(fileContents[0], string.Format("{0}\t{1} typed:", token.Timestamp, token.UserName));
        }

        [Test]
        public void TextFileRepositoryProperMessagesTest()
        {
            var message = TestHelpers.FakeMessage();
            var token = TestHelpers.FakeToken();

            _repository.Initialize(token);
            _repository.WriteMessage(message);

            string[] fileContents = File.ReadAllLines(_fileName);
            Assert.AreEqual(2, fileContents.Length);
            Assert.AreEqual(fileContents[1], string.Format("{0}\t{1}", message.Id, message.Text));
        }

        #endregion Public Methods

    }
}
