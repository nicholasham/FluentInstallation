using System;
using Microsoft.Web.Administration;

namespace FluentInstallation.IIS
{
    class ApplicationPoolConfigurer : IApplicationPoolConfigurer
    {
        private ApplicationPool _applicationPool;

        public ApplicationPoolConfigurer(ApplicationPool applicationPool)
        {
            
            if (applicationPool == null)
            {
                throw new ArgumentNullException("applicationPool");
            }

            _applicationPool = applicationPool;
        }

        public IApplicationPoolConfigurer Named(string name)
        {
            return Configure(x =>  x.Name = name);
        }

        public IApplicationPoolConfigurer UsingNetworkServiceIdentity()
        {
            return Configure(x => x.ProcessModel.IdentityType = ProcessModelIdentityType.NetworkService );
        }

        public IApplicationPoolConfigurer UsingApplicationPoolIdentity()
        {
            return Configure(x => x.ProcessModel.IdentityType = ProcessModelIdentityType.ApplicationPoolIdentity);
        }

        public IApplicationPoolConfigurer UsingLocalServiceIdentity()
        {
            return Configure(x => x.ProcessModel.IdentityType = ProcessModelIdentityType.LocalService);
        }
        
        public IApplicationPoolConfigurer UsingLocalSystemIdentity()
        {
            return Configure(x => x.ProcessModel.IdentityType = ProcessModelIdentityType.LocalSystem);
        }

        public IApplicationPoolConfigurer UsingCustomIdentity(string userName, string password)
        {
            return Configure(x =>
            {
                x.ProcessModel.IdentityType = ProcessModelIdentityType.SpecificUser;
                x.ProcessModel.UserName = userName;
                x.ProcessModel.Password = password;
            });
        }

        public IApplicationPoolConfigurer UsingClassicPipelineMode()
        {
            throw new NotImplementedException();
        }

        public IApplicationPoolConfigurer UsingIntegratedPipelineMode()
        {
            throw new NotImplementedException();
        }

        public IApplicationPoolConfigurer Configure(Action<ApplicationPool> action)
        {
            action(_applicationPool);
            return this;
        }
    }
}