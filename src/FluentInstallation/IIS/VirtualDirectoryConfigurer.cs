using System;
using Microsoft.Web.Administration;

namespace FluentInstallation.IIS
{
    public class VirtualDirectoryConfigurer : IVirtualDirectoryConfigurer
    {
        private readonly VirtualDirectory _virtualDirectory;

        public VirtualDirectoryConfigurer(VirtualDirectory virtualDirectory)
        {
            _virtualDirectory = virtualDirectory;
        }

        public IVirtualDirectoryConfigurer UsingAlias(string alias)
        {
            return ConfigureAdvancedOptions(x => x.Path = alias);
        }

        public IVirtualDirectoryConfigurer OnPhysicalPath(string path)
        {
            return ConfigureAdvancedOptions(x => x.PhysicalPath = path);
        }
        
        public IVirtualDirectoryConfigurer ConfigureAdvancedOptions(Action<VirtualDirectory> options)
        {
            options(_virtualDirectory);
            return this;
        }
    }
}