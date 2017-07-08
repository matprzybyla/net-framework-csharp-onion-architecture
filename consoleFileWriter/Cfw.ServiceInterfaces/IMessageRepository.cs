using Cfw.Domain;

namespace Cfw.ServiceInterfaces
{
    /// <summary>
    /// Defines a repository contract used by application. 
    /// This should be implemented in the Infrastructure layer and not in application Core.
    /// </summary>
    public interface IMessageRepository
    {
        /// <summary>
        /// Gets the value indicating whether repository was inialized.
        /// </summary>
        bool IsInitialized { get; }

        /// <summary>
        /// Initializes the repository with user data.
        /// </summary>
        /// <param name="token">The <see cref="IToken"/> object with "session" data.</param>
        void Initialize(IToken token);

        /// <summary>
        /// Writes a message into repository.
        /// </summary>
        /// <param name="message">The <see cref="IMessage"/> object with data.</param>
        void WriteMessage(IMessage message);
    }
}
