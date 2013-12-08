using System;
using Microsoft.Web.Administration;

namespace FluentInstallation.IIS
{
    public interface IApplicationPoolConfigurer : IFluentSyntax
    {
        IApplicationPoolConfigurer Named(string name);
        IApplicationPoolConfigurer UsingNetworkServiceIdentity();
        IApplicationPoolConfigurer UsingApplicationPoolIdentity();

        IApplicationPoolConfigurer UsingCustomIdentity(string userName, string password);
        IApplicationPoolConfigurer UsingClassicPipelineMode();

        IApplicationPoolConfigurer UsingLocalServiceIdentity();
        IApplicationPoolConfigurer UsingLocalSystemIdentity();
        IApplicationPoolConfigurer UsingIntegratedPipelineMode();

        IApplicationPoolConfigurer Configure(Action<ApplicationPool> action);
    }
}