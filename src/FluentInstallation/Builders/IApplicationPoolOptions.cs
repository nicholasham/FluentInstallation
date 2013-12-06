using System;
using Microsoft.Web.Administration;

namespace FluentInstallation.Builders
{
    public interface IApplicationPoolOptions : IFluentSyntax
    {
        IApplicationPoolOptions Named(string name);
        IApplicationPoolOptions UsingNetworkServiceIdentity();
        IApplicationPoolOptions UsingApplicationPoolIdentity();

        IApplicationPoolOptions UsingCustomIdentity(Action<ICustomIdentityOptions> identity);
        IApplicationOptions UsingClassicPipelineMode();
        IApplicationOptions UsingIntegratedPipelineMode();

        IWebsiteOptions ConfigureAdvancedOptions(Action<ApplicationPool> options);
    }
}