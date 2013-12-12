using System;
using Microsoft.Web.Administration;

namespace FluentInstallation.IIS
{
    public interface IApplicationPoolConfigurer : IFluentSyntax
    {
        IApplicationPoolConfigurer Named(string name);
        IApplicationPoolConfigurer UseNetworkServiceIdentity();
        IApplicationPoolConfigurer UseApplicationPoolIdentity();

        IApplicationPoolConfigurer UseCustomIdentity(string userName, string password);
        IApplicationPoolConfigurer UseClassicPipelineMode();

        IApplicationPoolConfigurer UseLocalServiceIdentity();
        IApplicationPoolConfigurer UseLocalSystemIdentity();
        IApplicationPoolConfigurer UseIntegratedPipelineMode();

        IApplicationPoolConfigurer Configure(Action<ApplicationPool> action);
    }
}