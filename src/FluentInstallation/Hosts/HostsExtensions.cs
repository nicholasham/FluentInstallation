namespace FluentInstallation.Hosts
{
    public static class HostsExtensions
    {
        public static IHostsConfigurer ConfigureHosts(this IInstallerContext context)
        {
            return new HostsConfigurer(); 
        }
            
    }
}