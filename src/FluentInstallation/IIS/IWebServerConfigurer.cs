using System;

namespace FluentInstallation.IIS
{
    public interface IWebServerConfigurer : IRootConfigurer
    {
        IWebServerConfigurer CreateApplicationPool(Action<IApplicationPoolConfigurer> options);
        IWebServerConfigurer CreateWebsite(Action<IWebsiteConfigurer> options);

        IWebServerConfigurer DeleteApplicationPool(string name);
        IWebServerConfigurer DeleteWebsite(string name);
        
        IWebServerConfigurer AlterWebsite(string name, Action<IWebsiteConfigurer> options);
    }
}