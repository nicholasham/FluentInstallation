using FluentInstallation.Web.Administration;
using NSubstitute;
using Xunit;

namespace FluentInstallation
{
    public class ConfigureExtensionsTests
    {
        [Fact]
        public void ConfigureWebServer_ConstructsAWebserverConfigurer()
        {
            var sut = Substitute.For<IInstallerContext>();
            var actual = sut.ConfigureWebServer();

            Assert.IsType<WebServerConfigurer>(actual);
        } 
    }
}