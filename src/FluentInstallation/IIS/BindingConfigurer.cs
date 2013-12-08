using System;
using Microsoft.Web.Administration;

namespace FluentInstallation.IIS
{
    public class BindingConfigurer : IBindingConfigurer
    {
        public BindingConfigurer(Binding binding)
        {
            
        }

        public IBindingConfigurer UsingProtocol(string protocol)
        {
            throw new NotImplementedException();
        }

        public IBindingConfigurer OnPort(int port)
        {
            throw new NotImplementedException();
        }

        public IBindingConfigurer UsingHostName(string hostName)
        {
            throw new NotImplementedException();
        }

        public IBindingConfigurer UseSslCertificate(string thumbprint)
        {
            throw new NotImplementedException();
        }

        public IBindingConfigurer ConfigureAdvancedOptions(Action<Binding> options)
        {
            throw new NotImplementedException();
        }
    }
}