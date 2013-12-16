using System;
using Microsoft.Web.Administration;

namespace FluentInstallation.Web
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

        public static Binding CreateBinding()
        {
            return CreateWebsite().Bindings.CreateElement();
        }

        public static Application CreateApplication()
        {
            return CreateWebsite().Applications.Add(string.Format("/{0}", Guid.NewGuid()), string.Empty);
        }

        public static VirtualDirectory CreateVirtualDirectory()
        {
            return CreateApplication().VirtualDirectories.Add(string.Format("/{0}", Guid.NewGuid()), string.Empty);
        }
    }
}