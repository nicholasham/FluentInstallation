using System;
using Microsoft.Web.Administration;

namespace FluentInstallation.IIS
{
    public interface IApplicationConfigurer : IFluentSyntax
    {
        IApplicationConfigurer UseAlias(string alias);
        IApplicationConfigurer OnPhysicalPath(string path);
        IApplicationConfigurer UseApplicationPool(string applicationPoolName);
        IApplicationConfigurer Configure(Action<Application> application);
    }
}