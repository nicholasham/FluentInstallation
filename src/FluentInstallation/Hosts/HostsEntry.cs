using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FluentInstallation.Hosts
{
    public class HostsEntry
    {
        public string Description { get; set; }
        public string IpAddress { get; set; }
        public string HostName { get; set; } 
    }

    public class HostsEntries : List<HostsEntry>
    {
        public static HostsEntries FromFile(string  path)
        {
            return new HostsEntries();      
        }
    }

}