using System;
using Microsoft.Web.Administration;

namespace FluentInstallation.IIS
{
    public class ApplicationConfigurer : IApplicationConfigurer
    {
        private Application _application;

        public ApplicationConfigurer(Application application)
        {
            _application = application;
        }

        public IApplicationConfigurer UsingAlias(string alias)
        {
            throw new NotImplementedException();
        }

        public IApplicationConfigurer OnPath(string path)
        {
            throw new NotImplementedException();
        }

        public IApplicationConfigurer UsingApplicationPool(string applicationPoolName)
        {
            throw new NotImplementedException();
        }

        public IApplicationConfigurer ConfigureAdvancedOptions(Action<Application> options)
        {
            throw new NotImplementedException();
        }
    }
}