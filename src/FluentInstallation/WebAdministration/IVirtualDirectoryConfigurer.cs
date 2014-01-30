using System;
using Microsoft.Web.Administration;

namespace FluentInstallation.WebAdministration
{
    public interface IVirtualDirectoryConfigurer : IFluentSyntax
    {
        IVirtualDirectoryConfigurer UseAlias(string alias);
        IVirtualDirectoryConfigurer OnPhysicalPath(string path);        
        IVirtualDirectoryConfigurer Configure(Action<VirtualDirectory> options);
    }


}


