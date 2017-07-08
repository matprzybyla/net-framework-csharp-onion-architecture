using Cfw.Bootstrapper;
using Cfw.Domain;
using System;
using System.Text;

namespace Cfw.UI
{
    internal class ConsoleReader
    {
        #region Private Fields

        private IToken _sessionToken;

        #endregion Private Fields

        #region Public Constructors

        public ConsoleReader()
        {
            InitializeConsole();
        }

        #endregion Public Constructors

        #region Internal Methods

        internal bool CreateSession()
        {
            Console.WriteLine("[CreateSession] Who are you?");
            string userName = ReadLineFromConsole();
            if (userName == null)
            {
                Console.WriteLine("BYE!");
                return false;
            }

            try
            {
                _sessionToken = StaticBootstrapper.TokenManager.CreateNew(userName);
                Console.WriteLine("Hello {0}, session started at {1} UTC", _sessionToken.UserName, _sessionToken.Timestamp);

                return true;
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }

            return false;
        }

        internal void ReadInput()
        {
            if (_sessionToken == null)
            {
                Console.WriteLine("Not logged in...");
            }

            Console.WriteLine("[ReadInput] Type message to log...");
            string messageText = ReadLineFromConsole();
            while (!string.IsNullOrEmpty(messageText))
            {
                bool logged = StaticBootstrapper.MessageService.WriteMessage(_sessionToken, new Message { Id = Guid.NewGuid(), Text = messageText });
                if (!logged)
                {
                    Console.WriteLine("Couldn't log message into repository");
                    Console.ReadLine();
                    break;
                }

                Console.WriteLine("[ReadInput] Type message to log...");
                messageText = ReadLineFromConsole();
            }

            Console.WriteLine("BYE!");
        }

        #endregion Internal Methods

        #region Private Methods

        private static void HandleError(Exception exception)
        {
            Console.WriteLine("Oops! Something went wrong with the app, here is the error message:");
            Exception ex = exception != null ? exception.InnerException ?? exception : null;
            Console.WriteLine(ex?.Message ?? "Not sure what was that...");
            Console.ReadLine();

            Environment.Exit(0);
        }

        private static void InitializeConsole()
        {
            Console.WriteLine("================================================================================");
            Console.WriteLine("ConsoleFileWriter uses Onion Architecture approach to write input into files");
            Console.WriteLine("This is realized in following steps:");
            Console.WriteLine("1. Create user session - give your name");
            Console.WriteLine("2. Read input from user - write it to active repository after ENTER");
            Console.WriteLine("3. Hit ESC or provide empty message at any time to finish the program");
            Console.WriteLine("================================================================================");
        }

        private static string ReadLineFromConsole()
        {
            StringBuilder builder = new StringBuilder();

            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter && info.Key != ConsoleKey.Escape)
            {
                Console.Write(info.KeyChar);
                builder.Append(info.KeyChar);
                info = Console.ReadKey(true);
            }

            if (info.Key != ConsoleKey.Enter)
            {
                return null;
            }

            Console.WriteLine();
            return builder.ToString();
        }

        #endregion Private Methods
    }
}
