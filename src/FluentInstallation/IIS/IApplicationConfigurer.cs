using System;
using Microsoft.Web.Administration;

namespace FluentInstallation.IIS
{
    public interface IApplicationConfigurer : IFluentSyntax
    {
        IApplicationConfigurer UsingAlias(string alias);
        IApplicationConfigurer OnPath(string path);
        IApplicationConfigurer UsingApplicationPool(string applicationPoolName);
        IApplicationConfigurer ConfigureAdvancedOptions(Action<Application> options);
    }
}