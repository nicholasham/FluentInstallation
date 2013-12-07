using System;
using Microsoft.Web.Administration;
using Xunit;
using Xunit.Sdk;

namespace FluentInstallation.IIS
{
    public class WebServerConfigurerTests
    {
        [Fact]
        public void SutIsIWebServerConfigurer()
        {
            var sut = new WebServerConfigurer();

            Assert.IsAssignableFrom<IWebServerConfigurer>(sut);
        }

        [Fact]
        public void CreateApplicationPool_ChainsCorrectly()
        {
            var sut = new WebServerConfigurer();
            var actual = sut.CreateApplicationPool(applicationPool => { });

            Assert.Equal(sut, actual);

        }

        [Fact]
        public void CreateApplicationPool_PassesNewApplicationPoolToOptionsAction()
        {
            var sut = new WebServerConfigurer();
            var actual = false;

            Action<IApplicationPoolOptions> action = (options) =>
            {
                actual = options != null;
            };

            
            sut.CreateApplicationPool(action);
            
            Assert.True(actual);
            
        }

        [Fact]
        public void CreateApplicationPool_CreatesANewApplicationPoolInServerManager()
        {
            var sut = new WebServerConfigurer();
            
            var expected= sut.ServerManager.ApplicationPools.Count + 1;

            sut.CreateApplicationPool((options)=>{});

            var actual = sut.ServerManager.ApplicationPools.Count;
            
            Assert.Equal(expected, actual);

        }
    }
}