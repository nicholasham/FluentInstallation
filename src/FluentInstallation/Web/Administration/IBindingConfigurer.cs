using System;
using Microsoft.Web.Administration;

namespace FluentInstallation.Web.Administration
{


    public interface IBindingConfigurer : IFluentSyntax
    {
        IBindingConfigurer UseProtocol(string protocol);
        IBindingConfigurer OnPort(int port);
        IBindingConfigurer UseHostName(string hostName);
        IBindingConfigurer UseCertificateWithThumbprint(string thumbprint);
        IBindingConfigurer UseIpAddress(string ipAddress);
        IBindingConfigurer Configure(Action<Binding> binding);

    }
}