using System;
using Microsoft.Web.Administration;

namespace FluentInstallation.IIS
{
    public interface IBindingConfigurer : IFluentSyntax
    {
        IBindingConfigurer UseProtocol(string protocol);
        IBindingConfigurer OnPort(int port);
        IBindingConfigurer UseHostName(string hostName);
        IBindingConfigurer UseSslCertificate(string thumbprint);
        IBindingConfigurer OnIpAddress(string ipAddress);
        IBindingConfigurer Configure(Action<Binding> options);

    }
}