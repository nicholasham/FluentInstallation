using FluentInstallation.Web;

namespace FluentInstallation
{
    public static class ConfigureExtensions
    {

        public static IWebServerConfigurer ConfigureWebServer(this IInstallerContext context)
        {
            return new WebServerConfigurer(new WrappedServerManager());
        }

    }
}