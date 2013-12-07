using System;
using Microsoft.Web.Administration;

namespace FluentInstallation.IIS
{
    class ApplicationPoolOptions : IApplicationPoolOptions
    {
        private ApplicationPool _applicationPool;

        public ApplicationPoolOptions(ApplicationPool applicationPool)
        {
            
            if (applicationPool == null)
            {
                throw new ArgumentNullException("applicationPool");
            }

            _applicationPool = applicationPool;
        }

        public IApplicationPoolOptions Named(string name)
        {
            throw new NotImplementedException();
        }

        public IApplicationPoolOptions UsingNetworkServiceIdentity()
        {
            throw new NotImplementedException();
        }

        public IApplicationPoolOptions UsingApplicationPoolIdentity()
        {
            throw new NotImplementedException();
        }

        public IApplicationPoolOptions UsingCustomIdentity(Action<ICustomIdentityOptions> identity)
        {
            throw new NotImplementedException();
        }

        public IApplicationOptions UsingClassicPipelineMode()
        {
            throw new NotImplementedException();
        }

        public IApplicationOptions UsingIntegratedPipelineMode()
        {
            throw new NotImplementedException();
        }

        public IWebsiteOptions ConfigureAdvancedOptions(Action<ApplicationPool> options)
        {
            throw new NotImplementedException();
        }
    }
}