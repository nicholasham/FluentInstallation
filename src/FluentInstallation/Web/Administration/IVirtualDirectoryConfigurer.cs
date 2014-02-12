using System;
using Microsoft.Web.Administration;

namespace FluentInstallation.Web.Administration
{
    public interface IVirtualDirectoryConfigurer : IFluentSyntax
    {
        IVirtualDirectoryConfigurer UseAlias(string alias);
        IVirtualDirectoryConfigurer OnPhysicalPath(string path);
        IVirtualDirectoryConfigurer UseWebProjectDirectoryAsPhysicalPath();
        IVirtualDirectoryConfigurer Configure(Action<VirtualDirectory> options);
    }


}


