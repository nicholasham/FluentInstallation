using System;
using System.IO;
using FluentInstallation.Hosts;

namespace FluentInstallation
{
    public static class HostsExtensions
    {
        public static IHostsConfigurer ConfigureHosts(this IInstallerContext context)
        {
            return new HostsConfigurer(new HostsFileRepository( () => GetHostsFileStream())); 
        }

        private static Stream GetHostsFileStream()
        {
            var windowsDirectoryPath = Environment.GetEnvironmentVariable("windir");
            var hostsFilePath = Path.Combine(windowsDirectoryPath, @"system32\drivers\etc\hosts");

            return new FileStream(hostsFilePath, FileMode.OpenOrCreate);
        }
    }
}