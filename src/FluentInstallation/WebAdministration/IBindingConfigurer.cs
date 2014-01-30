using System;
using Microsoft.Web.Administration;

namespace FluentInstallation.WebAdministration
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