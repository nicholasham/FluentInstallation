using System;

namespace FluentInstallation.Web
{
    public interface IWebServerConfigurer : IRootConfigurer
    {
        IWebServerConfigurer CreateApplicationPool(Action<IApplicationPoolConfigurer> configurer);
        IWebServerConfigurer CreateWebsite(Action<IWebsiteConfigurer> website);

        IWebServerConfigurer DeleteApplicationPool(string name);
        IWebServerConfigurer DeleteWebsite(string name);
        
        IWebServerConfigurer AlterWebsite(string name, Action<IWebsiteConfigurer> configurer);
    }
}