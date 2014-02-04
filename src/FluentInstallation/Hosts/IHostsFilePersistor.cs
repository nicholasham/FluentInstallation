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

            var matchPattern = @"^(?<IP>[0-9a-f.:]+)\s+(?<Name>[^\s#]+)(?<Description>.*)$";
            
            var hostsFile = new HostsFile();

            using (var reader = new StreamReader(stream))
            {

                while (reader.Peek() >= 0)
                {
                    var data = reader.ReadLine();
                    var matches = Regex.Matches(data, matchPattern);

                    foreach (Match match in matches)
                    {
                        var ip = match.Groups["IP"].ToString();
                        var name = match.Groups["Name"].ToString();
                        var description = match.Groups["Description"].ToString();

                        hostsFile.AddEntry(new HostEntry() {Ip = ip, Name = name, Description = description});

                    }

                }
                
                return hostsFile;
            }

            
            
        }

        public void Save(Stream stream, HostsFile entries)
        {
            throw new NotImplementedException();
        }
        
    }
}