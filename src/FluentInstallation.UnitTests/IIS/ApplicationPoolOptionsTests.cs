using System;
using Microsoft.Web.Administration;
using Xunit;

namespace FluentInstallation.IIS
{

    public class WebAdministrationFactory
    {
        public static ApplicationPool CreateApplicationPool()
        {
            var serverManager = new ServerManager();
            return serverManager.ApplicationPools.Add(Guid.NewGuid().ToString());
        }
    }

    public class ApplicationPoolOptionsTests
    {
        [Fact]
        public void SutIsIApplicationPoolOptions()
        {
            var sut = new ApplicationPoolOptions(WebAdministrationFactory.CreateApplicationPool());
            Assert.IsAssignableFrom<IApplicationPoolOptions>(sut);
        }

        [Fact]
        public void Contruct_ThrowsWhenApplicationPoolIsNull()
        {

            Assert.Throws<ArgumentNullException>(() => new ApplicationPoolOptions(null));
            
        }
    }
}