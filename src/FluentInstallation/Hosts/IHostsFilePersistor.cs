using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FluentInstallation.Hosts
{
    public interface IHostsFilePersistor
    {
        HostsFile Load(Stream stream);
        void Save(Stream stream, HostsFile entries);
    }

    public class HostsFilePersistor : IHostsFilePersistor
    {

        protected Func<Stream> CreateStream { get; set; }
        
        public HostsFile Load(Stream  stream)
        {

            var matchPattern = @"^(?<IP>[0-9a-f.:]+)\s+(?<HostName>[^\s#]+)(?<Comment>.*)$";
            
            var hostsFile = new HostsFile();

            using (var reader = new StreamReader(stream))
            {

                while (reader.Peek() >= 0)
                {
                    var data = reader.ReadLine();

                    if (data.StartsWith("#"))
                    {
                        hostsFile.AddComment(data.TrimStart('#'));
                    }
                    else
                    {
                        var matches = Regex.Matches(data, matchPattern);

                        foreach (Match match in matches)
                        {
                            var ip = match.Groups["IP"].ToString();
                            var hostName = match.Groups["HostName"].ToString();
                            var comment = match.Groups["Comment"].ToString().Replace("#", string.Empty).TrimStart();

                            hostsFile.AddEntry(new HostEntry() { Ip = ip, HostName = hostName, Comment = comment });

                        }
    
                    }
                    
                }
                
                return hostsFile;
            }

            
            
        }

        public void Save(Stream stream, HostsFile entries)
        {
            using (var writer = new StreamWriter(stream))
            {
                foreach (var comment in entries.AllComments())
                {
                    writer.Write("# {0}", comment);
                }

                foreach (var entry in entries.AllEntries())
                {
                    if (string.IsNullOrEmpty(entry.Comment))
                    {
                        writer.Write("{0} {1}", entry.Ip, entry.HostName);
                    }
                    else
                    {
                        writer.Write("{0} {1} #{2}", entry.Ip, entry.HostName, entry.Comment);
                        
                    }
                }
            }
        }
        
    }
}