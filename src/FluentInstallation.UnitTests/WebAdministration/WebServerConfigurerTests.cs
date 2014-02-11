using System;
using System.Linq;
using Microsoft.Web.Administration;
using NSubstitute;
using Xunit;

namespace FluentInstallation.WebAdministration
{
    public class WebServerConfigurerTests
    {
        [Fact]
        public void SutIsIWebServerConfigurer()
        {
            WebServerConfigurer sut = CreateSut();

            Assert.IsAssignableFrom<IWebServerConfigurer>(sut);
        }

        private static WebServerConfigurer CreateSut()
        {
            return new WebServerConfigurer(new TestLogger());
        }

        [Fact]
        public void AddApplicationPool_ChainsCorrectly()
        {
            var sut = CreateSut();
            IWebServerConfigurer actual = sut.AddApplicationPool(applicationPool => { });

            Assert.Equal(sut, actual);
        }

        [Fact]
        public void AddApplicationPool_PassesNewApplicationPoolToOptionsAction()
        {
            var sut = CreateSut();

            bool actual = false;

            Action<IApplicationPoolConfigurer> action = (options) => { actual = options != null; };


            sut.AddApplicationPool(action);

            Assert.True(actual);
        }

        [Fact]
        public void AddApplicationPool_AddsNewApplicationPoolToServerManager()
        {
            var sut = CreateSut();

            int expected = sut.ServerManager.ApplicationPools.Count + 1;

            sut.AddApplicationPool((options) => { });

            int actual = sut.ServerManager.ApplicationPools.Count;

            Assert.Equal(expected, actual);
            Assert.Equal("ApplicationPool" + expected, sut.ServerManager.ApplicationPools.Last().Name);
        }

        [Fact]
        public void RemoveApplicationPool_RemovesApplicationPoolFromServerManager()
        {
            var sut = CreateSut();

            ApplicationPool applicationPool = WebAdministrationFactory.CreateApplicationPool();
            sut.ServerManager.ApplicationPools.Add(applicationPool);

            sut.RemoveApplicationPool(applicationPool.Name);

            Assert.Equal(0, sut.ServerManager.ApplicationPools.Count(appPool => appPool.Name == applicationPool.Name));
        }
        
        [Fact]
        public void AddWebsite_CreatesWebsiteOnServerManager()
        {
            var sut = CreateSut();

            int expected = sut.ServerManager.Sites.Count + 1;

            sut.AddWebsite(options => { });

            int actual = sut.ServerManager.Sites.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RemoveWebsite_RemovesWebSiteFromTheServer()
        {
            var sut = CreateSut();
            Site webSite = WebAdministrationFactory.CreateWebsite();
            sut.ServerManager.Sites.Add(webSite);

            sut.RemoveWebsite(webSite.Name);

            Assert.Equal(0, sut.ServerManager.Sites.Count(site => site.Name == webSite.Name));
        }

        [Fact]
        public void Commit_AppliesChangesOnServer()
        {
            var serverManager = Substitute.For<IServerManager>();
            var sut = new WebServerConfigurer(Substitute.For<ILogger>(), serverManager); ;

            sut.Commit();

            serverManager.Received().CommitChanges();
        }

        [Fact]
        public void AlterWebsite_FindsFirstMatchingSiteAndPassesItToANewConfigurer()
        {
            var sut = CreateSut();

            Site expected = WebAdministrationFactory.CreateWebsite();
            sut.ServerManager.Sites.Add(expected);

            Site actual = default(Site);
          
            sut.AlterWebsite(expected.Name, configurer => configurer.Configure((site) => actual = site));

            Assert.Equal(expected.Name, actual.Name);
        }

        [Fact]
        public void AlterWebsite_ThrowsWhenUnableToMatchASiteWithTheSameName()
        {
            var sut = CreateSut();

            string randomName = Guid.NewGuid().ToString();

            Assert.Throws<InstallationException>(() => sut.AlterWebsite(randomName, (x) => { }));
        }
        
        [Fact]
        public void AssertWebsiteExists_ThrowsWhenNoWebsiteExistsWithTheGivenName()
        {
            var sut = CreateSut();

            Assert.Throws<InstallationException>(() => sut.AssertWebsiteExists("somerandomsitename"));
        }

        [Fact]
        public void AssertWebsiteExists_DoesNotThrowWhenItFindsASiteThatExistsWithTheGivenName()
        {
            var sut = CreateSut();

            Site expected = WebAdministrationFactory.CreateWebsite();
            sut.ServerManager.Sites.Add(expected);

            sut.AssertWebsiteExists(expected.Name);
        }


        [Fact]
        public void AssertApplicationPoolExists_ThrowsWhenNoApplicationPoolExistsWithTheGivenName()
        {
            var sut = CreateSut();

            Assert.Throws<InstallationException>(() => sut.AssertApplicationPoolExists("somerandomname"));
        }

        [Fact]
        public void AssertApplicationPoolExists_DoesNotThrowWhenItFindsAnApplicationPoolThatExistsWithTheGivenName()
        {
            var sut = CreateSut();

            ApplicationPool expected = WebAdministrationFactory.CreateApplicationPool();
            sut.ServerManager.ApplicationPools.Add(expected);

            sut.AssertApplicationPoolExists(expected.Name);
        }
    }
}