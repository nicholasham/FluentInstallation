using System;

namespace FluentInstallation.IIS
{
    public interface IWebServerConfigurer : IRootConfigurer
    {
        IWebServerConfigurer CreateApplicationPool(Action<IApplicationPoolConfigurer> options);
        IWebServerConfigurer CreateWebsite(Action<IWebsiteOptions> options);

        IWebServerConfigurer DeleteApplicationPool(string name);
        IWebServerConfigurer DeleteWebsite(string name);

        IDeleteApplicationOptions DeleteApplication(string name);

        IWebServerConfigurer AlterWebsite(string name, Action<IWebsiteOptions> options);
    }
}