using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentInstallation.Web.Hosts
{
    /// <summary>
    /// Represents a hosts file on a windows machine
    /// </summary>
    public class HostsFile
    {
        private readonly List<string> _comments;
        private readonly List<HostEntry> _hostEntries;

        public HostsFile()
        {
            _hostEntries = new List<HostEntry>();
            _comments = new List<string>();
        }

      public static HostsFile Load(Stream stream)
        {
            var hostsFile = new HostsFile();

            var reader = new StreamReader(stream);

            while (reader.Peek() >= 0)
            {
                string line = reader.ReadLine();

                HostEntry hostEntry;

                if (HostEntry.TryParse(line, out hostEntry))
                {
                    hostsFile.AddHostEntry(hostEntry);
                }
                else
                {
                    hostsFile.AddComment(line.TrimStart('#'));
                }
            }

            return hostsFile;
        }

        public IEnumerable<HostEntry> AllHostEntries()
        {
            return _hostEntries.ToArray();
        }


        public IEnumerable<string> AllComments()
        {
            return _comments.ToArray();
        }

        public IEnumerable<HostEntry> FindHostEntry(string hostName)
        {
            return _hostEntries.Where(
                entry => entry.HostName.Equals(hostName, StringComparison.InvariantCultureIgnoreCase))
                .ToArray();
        }

        public HostEntry AddHostEntry(HostEntry hostEntry)
        {
            var found = FindHostEntry(hostEntry.HostName);

            foreach (HostEntry entry in FindHostEntry(hostEntry.HostName))
            {
                entry.IsEnabled = false;
            }

            var duplicateEntry = found.FirstOrDefault(x => x.IpAddress.Equals(hostEntry.IpAddress));

            if (duplicateEntry == null)
            {
                _hostEntries.Add(hostEntry); 
            }
            else
            {
                duplicateEntry.IsEnabled = true;
            }
            
            return hostEntry;
        }

        public void RemoveHostEntry(HostEntry entry)
        {
            _hostEntries.Remove(entry);
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

        public void Save(Stream stream)
        {
            var writer = new StreamWriter(stream);

            foreach (string comment in AllComments())
            {
                writer.WriteLine("# {0}", comment);
            }

            foreach (HostEntry hostEntry in AllHostEntries())
            {
                writer.WriteLine(hostEntry.ToString());
            }

            writer.Flush();
        }
    }
}