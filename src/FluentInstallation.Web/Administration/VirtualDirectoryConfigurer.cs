﻿using System;
using System.IO;
using System.Reflection;
using Microsoft.Web.Administration;

namespace FluentInstallation.Web.Administration
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
            if (!Directory.Exists(path))
            {
               throw Exceptions.PhysicalPathDoesNotExist(path);
            }

            return Configure(x => x.PhysicalPath = path);
        }

        public IVirtualDirectoryConfigurer UseWebProjectDirectoryAsPhysicalPath()
        {
            return OnPhysicalPath(Assembly.GetCallingAssembly().ParentDirectoryPath());
        }

        public IVirtualDirectoryConfigurer Configure(Action<VirtualDirectory> options)
        {
            options(_virtualDirectory);
            return this;
        }
    }
}