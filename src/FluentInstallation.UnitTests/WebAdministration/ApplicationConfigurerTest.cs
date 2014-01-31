using Microsoft.Web.Administration;
using Xunit;

namespace FluentInstallation.WebAdministration
{
    public class ApplicationConfigurerTest
    {
        [Fact]
        public void SutIsApplicationConfigurer()
        {
            Assert.IsAssignableFrom<IApplicationConfigurer>(
                new ApplicationConfigurer(WebAdministrationFactory.CreateApplication()));
        }

        [Fact]
        public void Configure_GivesDirectAccessToTheWebsite()
        {
            var expected = WebAdministrationFactory.CreateApplication();
            var sut = new ApplicationConfigurer(expected);
            var actual = default(Application);

            sut.Configure(x => actual = x);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UseAlias_WithNoForwardSlashSetsThePathCorrectly()
        {
            var application = WebAdministrationFactory.CreateApplication();
            var sut = new ApplicationConfigurer(application);

            sut.UseAlias("TestAlias");

            Assert.Equal("/TestAlias", application.Path);
        }

        [Fact]
        public void UseAlias_WithForwardSlashSetsThePathCorrectly()
        {
            var application = WebAdministrationFactory.CreateApplication();
            var sut = new ApplicationConfigurer(application);

            sut.UseAlias("/TestAlias");

            Assert.Equal("/TestAlias", application.Path);
        }

        [Fact]
        public void OnPhysicalPath_SetsTheApplicationsVirtualDirectoryPhysicalPath()
        {
            var application = WebAdministrationFactory.CreateApplication();
            var sut = new ApplicationConfigurer(application);

            sut.OnPhysicalPath(@"C:\");

            Assert.Equal(@"C:\", application.VirtualDirectory().PhysicalPath);
        }
        
        [Fact]
        public void UseApplicationPool_SetsTheApplicationPoolName()
        {
            var application = WebAdministrationFactory.CreateApplication();
            var sut = new ApplicationConfigurer(application);

            sut.UseApplicationPool("TestApplicationPool");

            Assert.Equal("TestApplicationPool", application.ApplicationPoolName);

        }



    }
}