using System;
using Microsoft.Web.Administration;

namespace FluentInstallation.IIS
{
    public class VirtualDirectoryConfigurer : IVirtualDirectoryConfigurer
    {
        private VirtualDirectory _virtualDirectory;

        public VirtualDirectoryConfigurer(VirtualDirectory virtualDirectory)
        {
            _virtualDirectory = virtualDirectory;
        }

        public IVirtualDirectoryConfigurer UsingAlias(string alias)
        {
            throw new NotImplementedException();
        }

        public IVirtualDirectoryConfigurer OnPath(string path)
        {
            throw new NotImplementedException();
        }

        public IVirtualDirectoryConfigurer UsingApplicationPool(string applicationPoolName)
        {
            throw new NotImplementedException();
        }

        public IVirtualDirectoryConfigurer ConfigureAdvancedOptions(Action<VirtualDirectory> options)
        {
            throw new NotImplementedException();
        }
    }
}