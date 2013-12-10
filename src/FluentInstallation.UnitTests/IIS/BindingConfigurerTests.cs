using System;
using Microsoft.Web.Administration;
using Xunit;

namespace FluentInstallation.IIS
{
    public class BindingConfigurerTests
    {
        
        [Fact]
        public void SutIsBindingConfigurer()
        {
            var sut = new BindingConfigurer(WebAdministrationFactory.CreateBinding());
            Assert.IsAssignableFrom<IBindingConfigurer>(sut);
        }

        [Fact]
        public void Constructor_ThrowsWhenBindingIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new BindingConfigurer(null));
        }

        [Fact]
        public void Configure_GivesDirectAccessToTheWebsite()
        {
            var expected = WebAdministrationFactory.CreateBinding();
            var sut = new BindingConfigurer(expected);
            var actual = default(Binding);

            sut.Configure(x => actual = x);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UseProtocol_SetsTheBindingInformationCorrectly()
        {
            var binding = WebAdministrationFactory.CreateBinding();
            var sut = new BindingConfigurer(binding);
            
            sut.UseProtocol("https");

            Assert.Equal("https", binding.Protocol);
        }


        [Fact]
        public void OnPort_SetsTheBindingInformationCorrectly()
        {
            var binding = WebAdministrationFactory.CreateBinding();
            var sut = new BindingConfigurer(binding);

            sut.OnPort(99);

            Assert.Equal("*:99", binding.BindingInformation);
        }

        [Fact]
        public void OnIpAddress_SetsTheBindingInformationCorrectly()
        {
            var binding = WebAdministrationFactory.CreateBinding();
            var sut = new BindingConfigurer(binding);

            sut.OnIpAddress("172.168.7.9");

            Assert.Equal("172.168.7.9:80", binding.BindingInformation);
        }

        [Fact]
        public void UseHostName_SetsTheBindingInformationCorrectly()
        {
            var binding = WebAdministrationFactory.CreateBinding();
            var sut = new BindingConfigurer(binding);

            sut.UseHostName("mytestsite.com");

            Assert.Equal("*:80:mytestsite.com", binding.BindingInformation);
        }

    }
}