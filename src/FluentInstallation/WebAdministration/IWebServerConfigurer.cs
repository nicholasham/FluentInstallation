using System;

namespace FluentInstallation.WebAdministration
{
    public interface IWebServerConfigurer : IRootConfigurer
    {
        IWebServerConfigurer AddApplicationPool(Action<IApplicationPoolConfigurer> configurer);
        IWebServerConfigurer AddWebsite(Action<IWebsiteConfigurer> website);

        IWebServerConfigurer RemoveApplicationPool(string name);
        IWebServerConfigurer RemoveWebsite(string name);
        
        IWebServerConfigurer AlterWebsite(string name, Action<IWebsiteConfigurer> configurer);

        IWebServerConfigurer AssertWebsiteExists(string name);
        IWebServerConfigurer AssertApplicationPoolExists(string name);


    }
}