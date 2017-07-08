using Cfw.Domain;
using FakeItEasy;
using System;

namespace Cfw.Tests
{
    internal static class TestHelpers
    {
        #region Private Methods

        public static IMessage FakeInvalidMessage()
        {
            var message = A.Fake<IMessage>();

            A.CallTo(() => message.Id).Returns(Guid.Empty);
            A.CallTo(() => message.Text).Returns(null);

            return message;
        }

        public static IMessage FakeMessage()
        {
            var message = A.Fake<IMessage>();
            Guid id = Guid.NewGuid();

            A.CallTo(() => message.Id).Returns(id);
            A.CallTo(() => message.Text).Returns("test message");

            return message;
        }

        public static IToken FakeToken()
        {
            var token = A.Fake<IToken>();
            DateTime utc = DateTime.UtcNow;

            A.CallTo(() => token.Timestamp).Returns(utc);
            A.CallTo(() => token.UserName).Returns("SomeUserName");

            return token;
        }

        public static IToken FakeToken(DateTime utc)
        {
            var token = A.Fake<IToken>();
            A.CallTo(() => token.Timestamp).Returns(utc);
            A.CallTo(() => token.UserName).Returns("SomeUserName");

            return token;
        }

        #endregion Private Methods
    }
}
