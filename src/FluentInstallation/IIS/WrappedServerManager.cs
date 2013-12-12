using Microsoft.Web.Administration;

namespace FluentInstallation.IIS
{
    internal class WrappedServerManager : IServerManager
    {
        private readonly ServerManager _serverManager;        

        public WrappedServerManager()
        {
            _serverManager = new ServerManager();
        }

        public ApplicationPoolCollection ApplicationPools
        {
            get { return _serverManager.ApplicationPools; }
        }

        public SiteCollection Sites
        {
            get { return _serverManager.Sites; }
        }

        public void CommitChanges()
        {
            _serverManager.CommitChanges();            
        }
    }
}