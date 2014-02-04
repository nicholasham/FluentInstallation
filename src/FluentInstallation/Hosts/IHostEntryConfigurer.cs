using System;

namespace FluentInstallation.Hosts
{
    public interface IHostEntryConfigurer
    {
        IHostEntryConfigurer Configure(Action<HostEntry> hostEntry);
    }
}