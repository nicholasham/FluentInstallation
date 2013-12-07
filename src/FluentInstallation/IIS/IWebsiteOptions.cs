using System;
using Microsoft.Web.Administration;

namespace FluentInstallation.IIS
{
    public interface IWebsiteOptions : IFluentSyntax
    {
        IWebsiteOptions Named(string name);

        IWebsiteOptions UsingApplicationPool(string applicationPoolName);

        IWebsiteOptions UsingThisAssemblyDirectoryAsPath();

        IWebsiteOptions AddBinding(Action<IBindingOptions> binding);

        IWebsiteOptions AddApplication(Action<IApplicationOptions> application);


        IWebsiteOptions AddVirtualDirectory(Action<IApplicationOptions> application);


        IWebsiteOptions ConfigureAdvancedOptions(Action<Site> options);

      
    }
}