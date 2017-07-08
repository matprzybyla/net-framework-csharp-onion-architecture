using Cfw.Domain;

namespace Cfw.ServiceInterfaces
{
    /// <summary>
    /// Defines a message service with needed functions.
    /// This should be implemented in application Core as it will hold our message handling business logic.
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// Writes a message into repository used by service.
        /// </summary>
        /// <param name="userToken">The <see cref="IToken"/> object with current "session" to check if write is possible (session is active).</param>
        /// <param name="messageToWrite">The <see cref="IMessage"/> instance with data to write.</param>
        /// <returns></returns>
        bool WriteMessage(IToken userToken, IMessage messageToWrite);
    }
}
