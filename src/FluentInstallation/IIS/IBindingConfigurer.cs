using System;
using Microsoft.Web.Administration;

namespace FluentInstallation.IIS
{
    public interface IBindingConfigurer : IFluentSyntax
    {
        IBindingConfigurer UsingProtocol(string protocol);
        IBindingConfigurer OnPort(int port);
        IBindingConfigurer UsingHostName(string hostName);
        IBindingConfigurer UseSslCertificate(string thumbprint);

        IBindingConfigurer ConfigureAdvancedOptions(Action<Binding> options);

    }
}