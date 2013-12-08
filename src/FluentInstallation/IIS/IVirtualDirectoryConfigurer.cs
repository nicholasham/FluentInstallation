using System;
using Microsoft.Web.Administration;

namespace FluentInstallation.IIS
{
    public interface IVirtualDirectoryConfigurer : IFluentSyntax
    {
        IVirtualDirectoryConfigurer UsingAlias(string alias);
        IVirtualDirectoryConfigurer OnPath(string path);
        IVirtualDirectoryConfigurer UsingApplicationPool(string applicationPoolName);
        IVirtualDirectoryConfigurer ConfigureAdvancedOptions(Action<VirtualDirectory> options);
    }


}


