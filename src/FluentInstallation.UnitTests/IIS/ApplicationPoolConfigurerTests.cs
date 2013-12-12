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
        public void UseNetworkServiceIdentity_SetsIndentityCorrectly()
        {
            var applicationPool = WebAdministrationFactory.CreateApplicationPool();
            var sut = new ApplicationPoolConfigurer(applicationPool);

            sut.UseNetworkServiceIdentity();

            Assert.Equal(ProcessModelIdentityType.NetworkService, applicationPool.ProcessModel.IdentityType);
        }

        [Fact]
        public void UseApplicationPoolIdentity_SetsIndentityCorrectly()
        {
            var applicationPool = WebAdministrationFactory.CreateApplicationPool();
            var sut = new ApplicationPoolConfigurer(applicationPool);

            sut.UseApplicationPoolIdentity();

            Assert.Equal(ProcessModelIdentityType.ApplicationPoolIdentity, applicationPool.ProcessModel.IdentityType);
        }

        [Fact]
        public void UseLocalServiceIdentity_SetsIndentityCorrectly()
        {
            var applicationPool = WebAdministrationFactory.CreateApplicationPool();
            var sut = new ApplicationPoolConfigurer(applicationPool);

            sut.UseLocalServiceIdentity();

            Assert.Equal(ProcessModelIdentityType.LocalService, applicationPool.ProcessModel.IdentityType);
        }


        [Fact]
        public void UseLocalSystemIdentity_SetsIndentityCorrectly()
        {
            var applicationPool = WebAdministrationFactory.CreateApplicationPool();
            var sut = new ApplicationPoolConfigurer(applicationPool);

            sut.UseLocalSystemIdentity();

            Assert.Equal(ProcessModelIdentityType.LocalSystem, applicationPool.ProcessModel.IdentityType);

        }

        [Fact]
        public void UseCustomIdentity_SetsIndentityCorrectly()
        {
            var applicationPool = WebAdministrationFactory.CreateApplicationPool();
            var sut = new ApplicationPoolConfigurer(applicationPool);

            sut.UseCustomIdentity("TestUser","TestPassword");

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

        [Fact]
        public void UseClassicPipelineMode_SetsPipelineModeCorrectly()
        {
            var applicationPool = WebAdministrationFactory.CreateApplicationPool();
            var sut = new ApplicationPoolConfigurer(applicationPool);

            sut.UseClassicPipelineMode();

            Assert.Equal(ManagedPipelineMode.Classic, applicationPool.ManagedPipelineMode);
        }

        [Fact]
        public void UseIntegratedPipelineMode_SetsPipelineModeCorrectly()
        {
            var applicationPool = WebAdministrationFactory.CreateApplicationPool();
            var sut = new ApplicationPoolConfigurer(applicationPool);

            sut.UseIntegratedPipelineMode();

            Assert.Equal(ManagedPipelineMode.Integrated, applicationPool.ManagedPipelineMode);
        }
    }
  
}