using System;
using Microsoft.Web.Administration;
using Xunit;

namespace FluentInstallation.IIS
{
    public class ApplicationPoolConfigurerTests
    {
        [Fact]
        public void SutIsApplicationPoolConfigurer()
        {
            var sut = new ApplicationPoolConfigurer(WebAdministrationFactory.CreateApplicationPool());
            Assert.IsAssignableFrom<IApplicationPoolConfigurer>(sut);
        }

        [Fact]
        public void Contructor_ThrowsWhenApplicationPoolIsNull()
        {

            Assert.Throws<ArgumentNullException>(() => new ApplicationPoolConfigurer(null));
        }

        [Fact]
        public void Named_SetsTheName()
        {
            var applicationPool = WebAdministrationFactory.CreateApplicationPool();
            var sut = new ApplicationPoolConfigurer(applicationPool);
            
            sut.Named("TestApplicationPool");
            
            Assert.Equal("TestApplicationPool", applicationPool.Name);
        }

        [Fact]
        public void UsingNetworkServiceIdentity_SetsIndentityCorrectly()
        {
            var applicationPool = WebAdministrationFactory.CreateApplicationPool();
            var sut = new ApplicationPoolConfigurer(applicationPool);

            sut.UsingNetworkServiceIdentity();

            Assert.Equal(ProcessModelIdentityType.NetworkService, applicationPool.ProcessModel.IdentityType);
        }

        [Fact]
        public void UsingApplicationPoolIdentity_SetsIndentityCorrectly()
        {
            var applicationPool = WebAdministrationFactory.CreateApplicationPool();
            var sut = new ApplicationPoolConfigurer(applicationPool);

            sut.UsingApplicationPoolIdentity();

            Assert.Equal(ProcessModelIdentityType.ApplicationPoolIdentity, applicationPool.ProcessModel.IdentityType);
        }

        [Fact]
        public void UsingLocalServiceIdentity_SetsIndentityCorrectly()
        {
            var applicationPool = WebAdministrationFactory.CreateApplicationPool();
            var sut = new ApplicationPoolConfigurer(applicationPool);

            sut.UsingLocalServiceIdentity();

            Assert.Equal(ProcessModelIdentityType.LocalService, applicationPool.ProcessModel.IdentityType);
        }


        [Fact]
        public void UsingLocalSystemIdentity_SetsIndentityCorrectly()
        {
            var applicationPool = WebAdministrationFactory.CreateApplicationPool();
            var sut = new ApplicationPoolConfigurer(applicationPool);

            sut.UsingLocalSystemIdentity();

            Assert.Equal(ProcessModelIdentityType.LocalSystem, applicationPool.ProcessModel.IdentityType);

        }

        [Fact]
        public void UsingCustomIdentity_SetsIndentityCorrectly()
        {
            var applicationPool = WebAdministrationFactory.CreateApplicationPool();
            var sut = new ApplicationPoolConfigurer(applicationPool);

            sut.UsingCustomIdentity("TestUser","TestPassword");

            Assert.Equal(ProcessModelIdentityType.SpecificUser, applicationPool.ProcessModel.IdentityType);
            Assert.Equal("TestUser", applicationPool.ProcessModel.UserName);
            Assert.Equal("TestPassword", applicationPool.ProcessModel.Password);
        }

        [Fact]
        public void Configure_GivesDirectAccessToTheApplicationPool()
        {
            var expected = WebAdministrationFactory.CreateApplicationPool();
            var sut = new ApplicationPoolConfigurer(expected);
            var actual = default(ApplicationPool);

            sut.Configure(x => actual = x);

            Assert.Equal(expected, actual);
        }
    }
  
}