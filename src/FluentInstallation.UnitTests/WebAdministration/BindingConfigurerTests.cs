using System;
using System.Security.Cryptography.X509Certificates;
using FluentInstallation.Certificates;
using Microsoft.Web.Administration;
using NSubstitute;
using Xunit;

namespace FluentInstallation.WebAdministration
{

    public class CertificateFactory
    {
        public static X509Certificate2 CreateCertificate()
        {
            return new X509Certificate2(TestContext.GetResourceBytes("MyTestCertificate.pfx"), "test");
        }

    }

    public class BindingConfigurerTests
    {
        


        [Fact]
        public void SutIsBindingConfigurer()
        {
            var sut = new BindingConfigurer(WebAdministrationFactory.CreateBinding());
            Assert.IsAssignableFrom<IBindingConfigurer>(sut);
        }

        [Fact]
        public void Constructor_InitializesCertificateFinder()
        {
            var sut = new BindingConfigurer(WebAdministrationFactory.CreateBinding());
            Assert.IsType<CertificateFinder>(sut.CertificateFinder);
        }

        [Fact]
        public void Constructor_ThrowsWhenBindingIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new BindingConfigurer(null));
        }

        [Fact]
        public void Configure_GivesDirectAccessToTheWebsite()
        {
            Binding expected = WebAdministrationFactory.CreateBinding();
            var sut = new BindingConfigurer(expected);
            Binding actual = default(Binding);

            sut.Configure(x => actual = x);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UseProtocol_SetsTheBindingInformationCorrectly()
        {
            Binding binding = WebAdministrationFactory.CreateBinding();
            var sut = new BindingConfigurer(binding);

            sut.UseProtocol("https");

            Assert.Equal("https", binding.Protocol);
        }


        [Fact]
        public void OnPort_SetsTheBindingInformationCorrectly()
        {
            Binding binding = WebAdministrationFactory.CreateBinding();
            var sut = new BindingConfigurer(binding);

            sut.OnPort(99);

            Assert.Equal("*:99:", binding.BindingInformation);
        }

        [Fact]
        public void OnIpAddress_SetsTheBindingInformationCorrectly()
        {
            Binding binding = WebAdministrationFactory.CreateBinding();
            var sut = new BindingConfigurer(binding);

            sut.OnIpAddress("172.168.7.9");

            Assert.Equal("172.168.7.9:80:", binding.BindingInformation);
        }

        [Fact]
        public void UseHostName_SetsTheBindingInformationCorrectly()
        {
            Binding binding = WebAdministrationFactory.CreateBinding();
            var sut = new BindingConfigurer(binding);

            sut.UseHostName("mytestsite.com");

            Assert.Equal("*:80:mytestsite.com", binding.BindingInformation);
        }

        [Fact]
        public void UseCertificateWithThumbprint_SetsTheCertificateInformationCorrectly()
        {
            Binding binding = WebAdministrationFactory.CreateBinding();

            var finder = Substitute.For<ICertificateFinder>();
            var certificate = CertificateFactory.CreateCertificate();
            
            var result = new CertificateFindResult(StoreLocation.LocalMachine, StoreName.My, certificate);

            finder.Find(X509FindType.FindByThumbprint, certificate.Thumbprint).Returns(result);

            var sut = new BindingConfigurer(binding) {CertificateFinder = finder};

            sut.UseCertificateWithThumbprint(certificate.Thumbprint);

            Assert.Equal("https", binding.Protocol);
            Assert.Equal(certificate.GetCertHash(), binding.CertificateHash);
            Assert.Equal("My", binding.CertificateStoreName);
            

        }

        [Fact]
        public void UseCertificateWithThumbprint_ThrowsWhenNoCertificateExistsWIthTheThumbprint()
        {
            Binding binding = WebAdministrationFactory.CreateBinding();

            var finder = Substitute.For<ICertificateFinder>();
            
            var result = new CertificateFindResult();

            var thumbprint = "notfoundthumprint";

            finder.Find(X509FindType.FindByThumbprint, thumbprint).Returns(result);

            var sut = new BindingConfigurer(binding) { CertificateFinder = finder };

            Assert.Throws<InstallationException>(() => sut.UseCertificateWithThumbprint(thumbprint));
        }
    }
}