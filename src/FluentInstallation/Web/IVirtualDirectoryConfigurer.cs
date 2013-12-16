using System;
using Microsoft.Web.Administration;

namespace FluentInstallation.Web
{
    public interface IVirtualDirectoryConfigurer : IFluentSyntax
    {
        IVirtualDirectoryConfigurer UseAlias(string alias);
        IVirtualDirectoryConfigurer OnPhysicalPath(string path);        
        IVirtualDirectoryConfigurer Configure(Action<VirtualDirectory> options);
    }


}


