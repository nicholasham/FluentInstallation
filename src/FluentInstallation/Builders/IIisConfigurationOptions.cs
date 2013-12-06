using System;

namespace FluentInstallation.Builders
{
    public interface IIisConfigurationOptions : IConfigureOptions
    {
        IIisConfigurationOptions CreateApplicationPool(Action<IApplicationPoolOptions> applicationPool);
        IIisConfigurationOptions CreateWebsite(Action<IWebsiteOptions> site);

        IIisConfigurationOptions DeleteApplicationPool(string name);
        IIisConfigurationOptions DeleteWebsite(string name);

        IDeleteApplicationOptions DeleteApplication(string name);

        IIisConfigurationOptions AlterWebsite(string name, Action<IWebsiteOptions> site);
        
       
    }
}