namespace FluentInstallation.Hosts
{
    internal static class LoggingExtensions
    {
        internal static void ContructAddMessage(this HostEntry hostEntry, IMessageBuilder builder)
        {
            builder
                .WriteLine("Added Host Entry")
                .IncreaseIndent()
                .WriteLine("Host Name: {0}", hostEntry.HostName)
                .WriteLine("IP: {0}", hostEntry.IpAddress);
        }

        internal static void ContructRemoveMessage(this HostEntry hostEntry, IMessageBuilder builder)
        {
            builder
                .WriteLine("Removed Host Entry")
                .IncreaseIndent()
                .WriteLine("Host Name: {0}", hostEntry.HostName)
                .WriteLine("IP: {0}", hostEntry.IpAddress);
        }

    }
}