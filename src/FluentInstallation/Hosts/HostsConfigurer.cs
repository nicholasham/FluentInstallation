using System;
using System.IO;
using System.Linq;

namespace FluentInstallation.Hosts
{
    class HostsConfigurer : IHostsConfigurer
    {
        public HostsFile HostsFile { get; private set; }
        public ILogger Logger { get; private set; }


        private Func<Stream> GetStream { get; set; }
        
        public HostsConfigurer(ILogger logger, Func<Stream> getStream)
        {
            Logger = logger;
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
            Logger.Info(entry.ContructAddMessage);

            return this;
        }


        public IHostsConfigurer RemoveHostEntry(string hostName)
        {
            foreach (HostEntry entry in HostsFile.FindHostEntry(hostName))
            {
                HostsFile.RemoveHostEntry(entry);
                Logger.Info(entry.ContructAddMessage);
            }

            return this;
        }
    }
}