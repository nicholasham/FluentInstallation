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
        private readonly List<string> _comments;


        public HostsFile()
        {
            _hostEntries = new List<HostEntry>();
            _comments = new List<string>();
        }
        
        public IEnumerable<HostEntry> AllEntries()
        {
            return _hostEntries.ToArray();
        }


        public IEnumerable<string> AllComments()
        {
            return _comments.ToArray();
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

        public string AddComment(string comment)
        {
            _comments.Add(comment);
            return comment;
        }

        public void RemoveComment(string comment)
        {
            _comments.Remove(comment);
        }
      
    }

}