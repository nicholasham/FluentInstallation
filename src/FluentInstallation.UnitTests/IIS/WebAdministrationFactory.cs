using System;
using Microsoft.Web.Administration;

namespace FluentInstallation.IIS
{
    public class WebAdministrationFactory
    {
        public static ApplicationPool CreateApplicationPool()
        {
            var serverManager = new ServerManager();
            return serverManager.ApplicationPools.Add(Guid.NewGuid().ToString());
        }

        public static Site CreateWebsite()
        {
            var serverManager = new ServerManager();
            return serverManager.Sites.Add(Guid.NewGuid().ToString(), string.Empty, 80);
        }
    }
}