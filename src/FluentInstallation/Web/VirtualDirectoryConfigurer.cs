using System;
using Microsoft.Web.Administration;

namespace FluentInstallation.Web
{
    public class VirtualDirectoryConfigurer : IVirtualDirectoryConfigurer
    {
        private readonly VirtualDirectory _virtualDirectory;

        public VirtualDirectoryConfigurer(VirtualDirectory virtualDirectory)
        {
            _virtualDirectory = virtualDirectory;
        }

        public IVirtualDirectoryConfigurer UseAlias(string alias)
        {
            return Configure(x => x.Path = alias.ToPath());
        }

        public IVirtualDirectoryConfigurer OnPhysicalPath(string path)
        {
            return Configure(x => x.PhysicalPath = path);
        }
        
        public IVirtualDirectoryConfigurer Configure(Action<VirtualDirectory> options)
        {
            options(_virtualDirectory);
            return this;
        }
    }
}