using System;
using System.Linq;
using Microsoft.Web.Administration;

namespace FluentInstallation.Web.Administration
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

        public bool SiteExists(string name)
        {
            return _serverManager.Sites.Any(site => site.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
        }

        public bool ApplicationPoolExists(string name)
        {
            return
                _serverManager.ApplicationPools.Any(
                    applicationPool => applicationPool.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        public void CommitChanges()
        {
            _serverManager.CommitChanges();
        }
    }
}