namespace FluentInstallation.Web.Administration
{
   

    
    public static class WebAdministrationExtensions
    {

        public static IWebServerConfigurer ConfigureWebServer(this IInstallerContext context)
        {
            return new WebServerConfigurer(context.Logger, new WrappedServerManager());
        }


    }
}