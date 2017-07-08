using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cfw.Domain
{
    /// <summary>
    /// Represents used "session" token used for validation.
    /// </summary>
    public interface IToken
    {
        /// <summary>
        /// Gets the user name that simulates login in our sample.
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Gets the session timestamp checked for validity in our sample.
        /// </summary>
        DateTime Timestamp { get; }
    }
}
