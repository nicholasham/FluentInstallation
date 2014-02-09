using System;
using System.IO;
using System.Linq;

namespace FluentInstallation.Hosts
{
    class HostsConfigurer : IHostsConfigurer
    {
        public HostsFile HostsFile { get; private set; }

        private Func<Stream> GetStream { get; set; }
        
        public HostsConfigurer(Func<Stream> getStream )
        {
            GetStream = getStream;
            HostsFile = HostsFile.Load(GetStream());
        }

        public void Commit()
        {
            HostsFile.Save(GetStream());
        }

        public IHostsConfigurer AddHostEntry(Action<IHostEntryConfigurer> configurer)
        {
            var entry = new HostEntry();
            configurer(new HostEntryConfigurer(entry));

            HostsFile.AddHostEntry(entry);

            return this;
        }


        public IHostsConfigurer RemoveHostEntry(string hostName)
        {
            HostsFile.RemoveHostEntry(hostName);

            return this;
        }
    }
}