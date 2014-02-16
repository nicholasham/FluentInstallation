using System;

namespace FluentInstallation.Web.Hosts
{
    internal class HostEntryConfigurer : IHostEntryConfigurer
    {
        private readonly HostEntry _hostEntry;

        public HostEntryConfigurer(HostEntry hostEntry)
        {
            _hostEntry = hostEntry;
        }

        public IHostEntryConfigurer UseHostName(string hostName)
        {
            return Configure((hostEntry) => hostEntry.HostName = hostName);
        }

        public IHostEntryConfigurer UseLocalHostIpAddress()
        {
            return UseIpAddress(HostEntry.LocalHostIpAddress);
        }

        public IHostEntryConfigurer UseIpAddress(string ipAddress)
        {
            return Configure((hostEntry) => hostEntry.IpAddress = ipAddress);

        }

        public IHostEntryConfigurer Description(string description)
        {
            return Configure((hostEntry) => hostEntry.Description = description);

        }

        public IHostEntryConfigurer Configure(Action<HostEntry> action)
        {
            action(_hostEntry);
            return this;
        }
    }
}