using System;

namespace FluentInstallation.Hosts
{
    public interface IHostEntryConfigurer
    {
        IHostEntryConfigurer UseHostName(string hostName);
        IHostEntryConfigurer OnIpAddress(string ipAddress);
        IHostEntryConfigurer Description(string description);
        IHostEntryConfigurer Configure(Action<HostEntry> hostEntry);
    }
}