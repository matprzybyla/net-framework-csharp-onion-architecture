using NUnit.Framework;
using Cfw.Domain;
using Cfw.ServiceInterfaces;
using Cfw.Services;
using FakeItEasy;

namespace Cfw.Tests
{
    /// <summary>
    /// Tests MessageService implementation. 
    /// Uses IToken and IMessage faked abstractions to check each of the methods and edge cases.
    /// Uses faked IMessageRepository and ITokenManager dependencies to create instance of tested object. 
    /// We don't care how those dependencies will be implemented, the only that matters is if proper methods of faked objects were called in each case.
    /// </summary>
    [TestFixture]
    public class MessageServiceTests
    {
        #region Private Fields

        IMessageService _writerService;

        #endregion Private Fields

        #region Public Properties

        [Fake]
        public IMessageRepository FakeMessageRepository { get; set; }

        [Fake]
        public ITokenManager FakeTokenManager { get; set; }

        #endregion Public Properties

        #region Public Methods

        [SetUp]
        public void Initalize()
        {
            // start FakeItEasy framework
            Fake.InitializeFixture(this);

            _writerService = new MessageService(FakeTokenManager, FakeMessageRepository);            
        }

        [Test]
        public void WriterServiceInvalidMessageTest()
        {
            var token = TestHelpers.FakeToken();
            var msg = TestHelpers.FakeInvalidMessage();

            A.CallTo(() => FakeTokenManager.IsValid(A<IToken>.Ignored)).Returns(true);     // simulate that token check call passes
            bool success = _writerService.WriteMessage(token, msg);

            Assert.IsFalse(success);
            A.CallTo(() => FakeTokenManager.IsValid(A<IToken>.Ignored)).MustHaveHappened();
            A.CallTo(() => FakeMessageRepository.WriteMessage(A<IMessage>.Ignored)).MustNotHaveHappened();
        }

        [Test]
        public void WriterServiceInvalidTokenTest()
        {
            var msg = TestHelpers.FakeMessage();
            var token = TestHelpers.FakeToken();

            A.CallTo(() => FakeTokenManager.IsValid(A<IToken>.Ignored)).Returns(false);     // simulate that token check call does not pass
            bool success = _writerService.WriteMessage(token, msg);

            Assert.IsFalse(success);
            A.CallTo(() => FakeTokenManager.IsValid(A<IToken>.Ignored)).MustHaveHappened();
            A.CallTo(() => FakeMessageRepository.WriteMessage(A<IMessage>.Ignored)).MustNotHaveHappened();
        }

        [Test]
        public void WriterServiceNullMessageTest()
        {
            var token = TestHelpers.FakeToken();
            bool success = _writerService.WriteMessage(token, null);

            Assert.IsFalse(success);
            A.CallTo(() => FakeTokenManager.IsValid(A<IToken>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => FakeMessageRepository.WriteMessage(A<IMessage>.Ignored)).MustNotHaveHappened();
        }

        [Test]
        public void WriterServiceNullTokenTest()
        {
            var msg = TestHelpers.FakeMessage();
            bool success = _writerService.WriteMessage(null, msg);

            Assert.IsFalse(success);
            A.CallTo(() => FakeTokenManager.IsValid(A<IToken>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => FakeMessageRepository.WriteMessage(A<IMessage>.Ignored)).MustNotHaveHappened();
        }

        [Test]
        public void WriterServiceProperMessageTest()
        {
            var token = TestHelpers.FakeToken();
            var msg = TestHelpers.FakeMessage();

            A.CallTo(() => FakeTokenManager.IsValid(A<IToken>.Ignored)).Returns(true);     // simulate that token check call passes
            bool success = _writerService.WriteMessage(token, msg);

            Assert.IsTrue(success);
            A.CallTo(() => FakeTokenManager.IsValid(A<IToken>.Ignored)).MustHaveHappened();
            A.CallTo(() => FakeMessageRepository.WriteMessage(A<IMessage>.Ignored)).MustHaveHappened();
        }

        #endregion Public Methods
    }
}
