using System;

namespace FluentInstallation.Web.Hosts
{
    public interface IHostEntryConfigurer
    {
        IHostEntryConfigurer UseHostName(string hostName);
        IHostEntryConfigurer UseIpAddress(string ipAddress);
        IHostEntryConfigurer UseLocalHostIpAddress();
        IHostEntryConfigurer Description(string description);
        IHostEntryConfigurer Configure(Action<HostEntry> hostEntry);
    }
}