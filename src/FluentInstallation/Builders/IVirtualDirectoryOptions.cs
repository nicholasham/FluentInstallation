using System;
using System.Management.Automation;
using Microsoft.Web.Administration;

namespace FluentInstallation.Builders
{
    public interface IVirtualDirectoryOptions : IFluentSyntax
    {
        IVirtualDirectoryOptions UsingAlias(string alias);
        IVirtualDirectoryOptions OnPath(string path);
        IVirtualDirectoryOptions UsingApplicationPool(string applicationPoolName);
        IVirtualDirectoryOptions ConfigureAdvancedOptions(Action<VirtualDirectory> options);
    }


}


