using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Web.Administration;

namespace FluentInstallation.IIS
{


    public interface IBindingConfigurer : IFluentSyntax
    {
        IBindingConfigurer UseProtocol(string protocol);
        IBindingConfigurer OnPort(int port);
        IBindingConfigurer UseHostName(string hostName);
        IBindingConfigurer UseCertificateWithThumbprint(string thumbprint);
        IBindingConfigurer OnIpAddress(string ipAddress);
        IBindingConfigurer Configure(Action<Binding> binding);

    }
}