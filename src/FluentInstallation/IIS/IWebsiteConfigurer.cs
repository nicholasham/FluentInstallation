using System;
using Microsoft.Web.Administration;

namespace FluentInstallation.IIS
{
    public interface IWebsiteConfigurer : IFluentSyntax
    {
        IWebsiteConfigurer Named(string name);

        IWebsiteConfigurer UseApplicationPool(string applicationPoolName);

        IWebsiteConfigurer OnPhysicalPath(string path);

        IWebsiteConfigurer AddBinding(Action<IBindingConfigurer> binding);

        IWebsiteConfigurer AddApplication(Action<IApplicationConfigurer> application);


        IWebsiteConfigurer AddVirtualDirectory(Action<IVirtualDirectoryConfigurer> virtualDirectory);


        IWebsiteConfigurer Configure(Action<Site> action);

      
    }
}