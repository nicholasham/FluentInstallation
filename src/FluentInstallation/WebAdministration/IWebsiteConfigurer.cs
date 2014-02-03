using System;
using Microsoft.Web.Administration;

namespace FluentInstallation.WebAdministration
{
    public interface IWebsiteConfigurer : IFluentSyntax
    {
        IWebsiteConfigurer WithId(int id);

        IWebsiteConfigurer Named(string name);

        IWebsiteConfigurer UseApplicationPool(string applicationPoolName);

        IWebsiteConfigurer OnPhysicalPath(string path);

        IWebsiteConfigurer UseWebProjectDirectoryAsPhysicalPath();

        IWebsiteConfigurer AddBinding(Action<IBindingConfigurer> action);

        IWebsiteConfigurer RemoveApplication(string alias);
        
        IWebsiteConfigurer RemoveVirtualDirectory(string alias);

        IWebsiteConfigurer AddApplication(Action<IApplicationConfigurer> application);
        
        IWebsiteConfigurer AddVirtualDirectory(Action<IVirtualDirectoryConfigurer> virtualDirectory);

        IWebsiteConfigurer AssertApplicationExists(string alias);

        IWebsiteConfigurer AssertVirtualDirectoryExists(string alias);

        IWebsiteConfigurer Configure(Action<Site> action);
      
    }
}