using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FluentInstallation.WebAdministration;

namespace FluentInstallation.Hosts
{
    public class HostsFile 
    {
        private readonly List<HostEntry> _hostEntries;

        public HostsFile()
        {
            _hostEntries = new List<HostEntry>();
        }
        
        public IEnumerable<HostEntry> AllEntries()
        {
            return _hostEntries.ToArray();
        }

        public HostEntry AddEntry(HostEntry hostEntry)
        {
            _hostEntries.Add(hostEntry);
            return hostEntry;
        }

        public void RemoveEntry(HostEntry hostEntry)
        {
            _hostEntries.Remove(hostEntry);
        }

      
    }

}