using Cfw.Domain;
using Cfw.ServiceInterfaces;
using Cfw.Services;
using FakeItEasy;
using NUnit.Framework;
using System;

namespace Cfw.Tests
{
    /// <summary>
    /// Tests TokenManager implementation. 
    /// Uses IToken faked abstraction to check each of the methods and edge cases.
    /// </summary>
    [TestFixture]
    public class TokenManagerTests
    {

        #region Private Fields

        ITokenManager _tokenManager;

        #endregion Private Fields

        #region Public Methods

        [SetUp]
        public void Initalize()
        {
            _tokenManager = new TokenManager();
        }

        [Test]
        public void TokenManagerCreateNewExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(() => _tokenManager.CreateNew(null));
        }

        [Test]
        public void TokenManagerCreateNewProperTimestampTest()
        {
            IToken token = _tokenManager.CreateNew("SomeUserName");
            Assert.LessOrEqual(token.Timestamp, DateTime.UtcNow);
        }

        [Test]
        [TestCase("")]
        [TestCase("SomeUserName")]
        public void TokenManagerCreateNewTest(string userName)
        {
            IToken token = _tokenManager.CreateNew(userName);
            Assert.AreEqual(string.IsNullOrEmpty(userName) ? "Nobody" : userName, token.UserName);
        }

        [Test]
        public void TokenManagerIsValidNullTest()
        {
            Assert.IsFalse(_tokenManager.IsValid(null));
        }

        [Test]
        [TestCase("")]
        [TestCase("SomeUserName")]
        public void TokenManagerIsValidProperNameTest(string userName)
        {
            IToken token = TestHelpers.FakeToken(DateTime.UtcNow);
            Assert.IsTrue(_tokenManager.IsValid(token));
        }

        [Test]
        public void TokenManagerIsValidProperTimestampTest()
        {
            IToken token = TestHelpers.FakeToken(DateTime.MaxValue);
            Assert.IsFalse(_tokenManager.IsValid(token));
        }

        [Test]
        public void TokenManagerIsValidWrongNameTest()
        {
            IToken token = TestHelpers.FakeToken(DateTime.UtcNow);
            A.CallTo(() => token.UserName).Returns(null);
            Assert.IsFalse(_tokenManager.IsValid(token));
        }

        #endregion Public Methods

    }
}
