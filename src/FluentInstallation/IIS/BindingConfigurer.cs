using System;
using Microsoft.Web.Administration;

namespace FluentInstallation.IIS
{
    public class BindingConfigurer : IBindingConfigurer
    {
        private readonly Binding _binding;

        public BindingConfigurer(Binding binding)
        {
            if (binding == null)
            {
                throw new ArgumentNullException("binding");
            }

            _binding = binding;

        }

        public IBindingConfigurer UseProtocol(string protocol)
        {
            return Configure(binding => binding.Protocol = protocol);
        }

        public IBindingConfigurer OnPort(int port)
        {

            return Configure(binding =>
                {
                    var information = BindingInformation.Parse(binding.BindingInformation);
                    information.Port = port;
                    binding.BindingInformation = information.ToString();
                } );
        }

        public IBindingConfigurer UseHostName(string hostName)
        {
            return Configure(binding =>
            {
                var information = BindingInformation.Parse(binding.BindingInformation);
                information.HostName = hostName;
                binding.BindingInformation = information.ToString();
            });
        }

        public IBindingConfigurer UseSslCertificate(string thumbprint)
        {
            throw new NotImplementedException();
        }

        public IBindingConfigurer OnIpAddress(string ipAddress)
        {
            return Configure(binding =>
            {
                var information = BindingInformation.Parse(binding.BindingInformation);
                information.IpAddress = ipAddress;
                binding.BindingInformation = information.ToString();
            });
        }

        public IBindingConfigurer Configure(Action<Binding> action)
        {
            action(_binding);
            return this;
        }
    }
}