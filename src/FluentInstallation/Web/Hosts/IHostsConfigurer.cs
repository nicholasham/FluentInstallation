using System;

namespace FluentInstallation.Web.Hosts
{
    public interface IHostsConfigurer : IRootConfigurer
    {
        IHostsConfigurer AddHostEntry(Action<IHostEntryConfigurer> configurer);
        IHostsConfigurer RemoveHostEntry(string hostName);
    }
}