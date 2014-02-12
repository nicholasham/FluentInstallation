using System;
using Microsoft.Web.Administration;

namespace FluentInstallation.Web.Administration
{
    class ApplicationPoolConfigurer : IApplicationPoolConfigurer
    {
        private readonly ApplicationPool _applicationPool;

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
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            return Configure(x =>  x.Name = name);
        }

        public IApplicationPoolConfigurer UseNetworkServiceIdentity()
        {
            return Configure(x => x.ProcessModel.IdentityType = ProcessModelIdentityType.NetworkService );
        }

        public IApplicationPoolConfigurer UseApplicationPoolIdentity()
        {
            return Configure(x => x.ProcessModel.IdentityType = ProcessModelIdentityType.ApplicationPoolIdentity);
        }

        public IApplicationPoolConfigurer UseLocalServiceIdentity()
        {
            return Configure(x => x.ProcessModel.IdentityType = ProcessModelIdentityType.LocalService);
        }
        
        public IApplicationPoolConfigurer UseLocalSystemIdentity()
        {
            return Configure(x => x.ProcessModel.IdentityType = ProcessModelIdentityType.LocalSystem);
        }

        public IApplicationPoolConfigurer UseCustomIdentity(string userName, string password)
        {
            return Configure(x =>
            {
                x.ProcessModel.IdentityType = ProcessModelIdentityType.SpecificUser;
                x.ProcessModel.UserName = userName;
                x.ProcessModel.Password = password;
            });
        }

        public IApplicationPoolConfigurer UseClassicPipelineMode()
        {
            return Configure(x => x.ManagedPipelineMode = ManagedPipelineMode.Classic);
        }

        public IApplicationPoolConfigurer UseIntegratedPipelineMode()
        {
            return Configure(x => x.ManagedPipelineMode = ManagedPipelineMode.Integrated);
        }

        public IApplicationPoolConfigurer Configure(Action<ApplicationPool> action)
        {
            action(_applicationPool);
            return this;
        }
    }
}