using System;
using System.Linq;

namespace FluentInstallation.Hosts
{
    class HostsConfigurer : IHostsConfigurer
    {
        private IHostsFileRepository _hostsFileRepository;
        private HostsFile _hostsFile;

        public HostsConfigurer(IHostsFileRepository hostsFileRepository)
        {
            _hostsFileRepository = hostsFileRepository;
            _hostsFile = hostsFileRepository.Load();
        }


        public void Commit()
        {
            _hostsFileRepository.Save(_hostsFile);
        }

        public IHostsConfigurer AddHostEntry(Action<IHostEntryConfigurer> configurer)
        {
            var entry = new HostEntry();
            configurer(new HostEntryConfigurer(entry));

            _hostsFile.AddEntry(entry);

            return this;
        }


        public IHostsConfigurer RemoveHostEntry(string hostName)
        {
            var entry =
                _hostsFile.AllEntries()
                          .FirstOrDefault(x => x.HostName.Equals(hostName, StringComparison.InvariantCultureIgnoreCase));

            if (entry != null)
            {
                _hostsFile.RemoveEntry(entry);
            }

            return this;
        }
    }
}