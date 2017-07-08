using Cfw.Domain;

namespace Cfw.ServiceInterfaces
{
    /// <summary>
    /// Defines a token validator contract.
    /// This should be implemented in Infrastructure layer as it will hold our validation business logic.
    /// </summary>
    public interface ITokenManager
    {
        /// <summary>
        /// Creates a new "session" token based on user name.
        /// </summary>
        /// <param name="userName">The name of a user for session</param>
        /// <returns>New <see cref="IToken"/> object.</returns>
        IToken CreateNew(string userName);

        /// <summary>
        /// Checks if passed "session" token is valid.
        /// </summary>
        /// <param name="token">The <see cref="IToken"/> object to be checked.</param>
        /// <returns>True if valid, false otherwise.</returns>
        bool IsValid(IToken token);
    }
}
