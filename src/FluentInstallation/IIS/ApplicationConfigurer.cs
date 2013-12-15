using System;
using Microsoft.Web.Administration;

namespace FluentInstallation.IIS
{
    public class ApplicationConfigurer : IApplicationConfigurer
    {
        private readonly Application _application;

        public ApplicationConfigurer(Application application)
        {
            _application = application;
        }

        public IApplicationConfigurer UseAlias(string alias)
        {

            return Configure(application =>
            {
                application.Path = alias.StartsWith("/") ? alias : "/" + alias;        
            });
            
        }

        public IApplicationConfigurer OnPhysicalPath(string path)
        {
            return Configure(application =>
            {
                application.VirtualDirectory().PhysicalPath = path ;
            });
        }

        public IApplicationConfigurer UseApplicationPool(string applicationPoolName)
        {
            return Configure(application =>
            {
                application.ApplicationPoolName = applicationPoolName;
            });
        }

        public IApplicationConfigurer Configure(Action<Application> application)
        {
            application(_application);
            return this;
        }
    }
}