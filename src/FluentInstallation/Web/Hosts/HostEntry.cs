using System.Text.RegularExpressions;

namespace FluentInstallation.Web.Hosts
{
    /// <summary>
    /// Represents a host file entry
    /// </summary>
    public class HostEntry
    {
        public const string LocalHostIpAddress = "127.0.0.1";
        public string Description { get; set; }
        public string IpAddress { get; set; }
        public string HostName { get; set; }
        public bool IsEnabled { get; set; }

        public HostEntry()
        {
            IpAddress = LocalHostIpAddress;
            IsEnabled = true;
        }

        public bool HasDescription { get { return !string.IsNullOrEmpty(Description); } }

        public static bool TryParse(string value, out HostEntry hostEntry)
        {
            const string matchPattern = @"^(?<IpAddress>[0-9a-f.:]+)\s+(?<HostName>[^\s#]+)(?<Description>.*)$";

            hostEntry = new HostEntry();

            var regex = new Regex(matchPattern);
            var match = regex.Match(value.TrimStart('#'));

            if (match.Success)
            {
               hostEntry = new HostEntry()
               {
                   IpAddress = match.Groups["IpAddress"].Value,
                   HostName = match.Groups["HostName"].Value,
                   Description = match.Groups["Description"].Value.Replace("#", string.Empty).Trim(),
                   IsEnabled = !value.StartsWith("#")
               };
            }

            return match.Success;
        }

        public override string ToString()
        {
            var hostEntryFormat = "{0,-16}{1}{2}";
            var descriptionFormat = "\t# {0}";

            var ipAddress = IpAddress;
            var hostName = HostName;
            var description = HasDescription ? string.Format(descriptionFormat, Description) : string.Empty;

            var value = string.Format(hostEntryFormat, ipAddress, hostName, description);

            return  IsEnabled ? value : string.Format("# {0}", value) ;

        }
    }
}