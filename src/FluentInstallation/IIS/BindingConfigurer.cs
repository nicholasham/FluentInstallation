using System;
using System.Security.Cryptography.X509Certificates;
using FluentInstallation.Certificates;
using Microsoft.Web.Administration;

namespace FluentInstallation.IIS
{

    public class BindingConfigurer : IBindingConfigurer
    {
        private readonly Binding _binding;

        internal ICertificateFinder CertificateFinder { get; set; }

        public BindingConfigurer(Binding binding)
        {
            if (binding == null)
            {
                throw new ArgumentNullException("binding");
            }

            _binding = binding;


            CertificateFinder = new CertificateFinder();
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

        
        public IBindingConfigurer UseCertificateWithThumbprint(string thumbprint)
        {
            return Configure(binding =>
            {
                var result = CertificateFinder.Find(X509FindType.FindByThumbprint, thumbprint);

                if (!result.Found)
                {
                   throw Exceptions.NoCertificateFoundMatchingThumbprint(thumbprint);
                }

                binding.Protocol = "https";
                binding.CertificateStoreName = result.StoreName.ToString();
                binding.CertificateHash = result.Certificate.GetCertHash();
            });
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