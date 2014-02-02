namespace FluentInstallation.WebAdministration
{
   

    
    public static class WebAdministrationExtensions
    {

        public static IWebServerConfigurer ConfigureWebServer(this IInstallerContext context)
        {
            return new WebServerConfigurer(context.Logger, new WrappedServerManager());
        }


    }
}