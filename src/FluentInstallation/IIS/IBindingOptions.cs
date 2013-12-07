using System;
using Microsoft.Web.Administration;

namespace FluentInstallation.IIS
{
    public interface IBindingOptions : IFluentSyntax
    {
        IBindingOptions UsingProtocol(string protocol);
        IBindingOptions OnPort(int port);
        IBindingOptions UsingHostName(string hostName);
        IBindingOptions UseSslCertificate(string thumbprint);

        IBindingOptions ConfigureAdvancedOptions(Action<Binding> options);

    }
}