using System;

namespace FluentInstallation.Hosts
{
    public class HostsConfigurer : IHostsConfigurer
    {
        
        public void Commit()
        {
            
        }

        public IHostsConfigurer AddHostEntry(Action<IHostEntryConfigurer> configurer)
        {
            return this;
        }

        public IHostsConfigurer RemoveHostEntry(string hostName)
        {
            throw new NotImplementedException();
        }
    }
}