using System;
using System.IO;
using System.Reflection;
using Microsoft.Web.Administration;

namespace FluentInstallation.Web.Administration
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
                application.Path = alias.ToPath();        
            });
            
        }

        public IApplicationConfigurer OnPhysicalPath(string path)
        {
            if (!Directory.Exists(path))
            {
                throw Exceptions.PhysicalPathDoesNotExist(path);
            }

            return Configure(application =>
            {
                application.VirtualDirectory().PhysicalPath = path ;
            });
        }

        public IApplicationConfigurer UseWebProjectDirectoryAsPhysicalPath()
        {
            return OnPhysicalPath(Assembly.GetCallingAssembly().ParentDirectoryPath());
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