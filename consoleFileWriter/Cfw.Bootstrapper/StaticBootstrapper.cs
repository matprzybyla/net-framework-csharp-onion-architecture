using Cfw.Infrastructure;
using Cfw.ServiceInterfaces;
using Cfw.Services;
using SimpleInjector;

namespace Cfw.Bootstrapper
{
    public static class StaticBootstrapper
    {

        #region Private Fields

        private static Container _injectorContainer;
        private static object _lock = new object();

        #endregion Private Fields

        #region Public Constructors

        static StaticBootstrapper()
        {
            if (_injectorContainer == null)
            {
                lock (_lock)
                {
                    if (_injectorContainer == null)
                    {
                        Bootstrap();
                    }
                }
            }
        }

        #endregion Public Constructors

        #region Public Properties

        public static IMessageService MessageService => _injectorContainer.GetInstance<IMessageService>();

        public static ITokenManager TokenManager => _injectorContainer.GetInstance<ITokenManager>();

        #endregion Public Properties

        #region Private Methods

        /// <summary>
        /// Initializes the dependency injector with all needed objects and interfaces.
        /// </summary>
        private static void Bootstrap()
        {
            _injectorContainer = new Container();
            
            _injectorContainer.Register<ITokenManager, TokenManager>();
            _injectorContainer.Register<IMessageService, MessageService>();

            // comment/uncommenct the one of following lines to change repository implementation used in app
            // you can also pass a file name used by created instance using constructor argument
            //_injectorContainer.Register<IMessageRepository>(() => new XmlFileRepository(string.Empty), Lifestyle.Singleton);
            _injectorContainer.Register<IMessageRepository>(() => new TextFileRepository(string.Empty), Lifestyle.Singleton);

            // container composition verification
            _injectorContainer.Verify();
        }

        #endregion Private Methods

    }
}
