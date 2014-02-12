using System;
using System.IO;

namespace FluentInstallation.Web.Hosts
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
            HostsFile = Load();
        }

        HostsFile Load()
        {
            using (var stream  = GetStream())
            {
                return HostsFile.Load(stream);
            }
        }

        public void Commit()
        {
            using (var stream = GetStream())
            {
                HostsFile.Save(stream);
            }
            
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