using System;

namespace Cfw.Domain
{
    /// <summary>
    /// Represents message to be saved in repository.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Gets the message text to save.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Gets the unique message identifier.
        /// </summary>
        Guid Id { get; set; }
    }
}
