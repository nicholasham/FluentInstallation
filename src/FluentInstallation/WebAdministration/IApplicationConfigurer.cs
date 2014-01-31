using System;
using Microsoft.Web.Administration;

namespace FluentInstallation.WebAdministration
{
    public interface IApplicationConfigurer : IFluentSyntax
    {
        IApplicationConfigurer UseAlias(string alias);
        IApplicationConfigurer OnPhysicalPath(string path);
        IApplicationConfigurer UseApplicationPool(string applicationPoolName);
        IApplicationConfigurer UseWebProjectDirectoryAsPhysicalPath();
        IApplicationConfigurer Configure(Action<Application> application);
    }
}