using System;

namespace FluentInstallation.Hosts
{
    public interface IHostsConfigurer : IRootConfigurer
    {
        IHostsConfigurer AddHostEntry(Action<IHostEntryConfigurer> configurer);
        IHostsConfigurer RemoveHostEntry(string hostName);
    }
}