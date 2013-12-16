using System;
using System.Linq;
using Microsoft.Web.Administration;
using NSubstitute;
using Xunit;

namespace FluentInstallation.Web
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

            Action<IApplicationPoolConfigurer> action = (options) =>
            {
                actual = options != null;
            };

            
            sut.CreateApplicationPool(action);
            
            Assert.True(actual);
            
        }
        
        [Fact]
        public void CreateApplicationPool_AddsNewApplicationPoolToServerManager()
        {
            var sut = new WebServerConfigurer();
            
            var expected = sut.ServerManager.ApplicationPools.Count + 1;

            sut.CreateApplicationPool((options)=>{});

            var actual = sut.ServerManager.ApplicationPools.Count;
            
            Assert.Equal(expected, actual);
            Assert.Equal("ApplicationPool" + expected, sut.ServerManager.ApplicationPools.Last().Name);

        }

        [Fact]
        public void DeleteApplicationPool_DeletesApplicationPoolFromServerManager()
        {
            var sut = new WebServerConfigurer();
            var applicationPool = WebAdministrationFactory.CreateApplicationPool();
            sut.ServerManager.ApplicationPools.Add(applicationPool);

            sut.DeleteApplicationPool(applicationPool.Name);

            Assert.Equal(0, sut.ServerManager.ApplicationPools.Count(appPool => appPool.Name == applicationPool.Name));

        }

        [Fact]
        public void CreateWebsite_CreatesWebsiteOnServerManager()
        {
            var sut = new WebServerConfigurer();

            var expected = sut.ServerManager.Sites.Count + 1;

            sut.CreateWebsite(options => { });

            var actual = sut.ServerManager.Sites.Count;

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void DeleteWebsite_DeletesWebSiteFromTheServerManager()
        {
            var sut = new WebServerConfigurer();
            var webSite = WebAdministrationFactory.CreateWebsite();
            sut.ServerManager.Sites.Add(webSite);

            sut.DeleteWebsite(webSite.Name);

            Assert.Equal(0, sut.ServerManager.Sites.Count(site => site.Name == webSite.Name));
        }

        [Fact]
        public void Commit_AppliesChangesOnServerManager()
        {
            var serverManager = Substitute.For<IServerManager>();
            var sut = new WebServerConfigurer(serverManager);

            sut.Commit();

            serverManager.Received().CommitChanges();
        }

        [Fact]
        public void AlterWebsite_FindsFirstMatchingSiteAndPassesItToANewConfigurer()
        {
            var sut = new WebServerConfigurer();
            var configurer = Substitute.For<IWebsiteConfigurer>();

           
            var expected = WebAdministrationFactory.CreateWebsite();
            sut.ServerManager.Sites.Add(expected);

            Site actual = default(Site);
            WebServerConfigurer.CreateWebsiteConfigurer = (x) => { actual = x; return configurer; };  
            
            sut.AlterWebsite(expected.Name, website => {});

            Assert.Equal(expected.Name, actual.Name);

        }
        
        [Fact]
        public void AlterWebsite_ThrowsWhenUnableToMatchASiteWithTheSameName()
        {
            var sut = new WebServerConfigurer();

            var randomName = Guid.NewGuid().ToString();

            Assert.Throws<InstallationException>(() => sut.AlterWebsite(randomName, (x) => { }));

        }

        [Fact]
        public void AlterAppPool_FindsFirstMatchingAppPoolAndPassesItToANewConfigurer()
        {
            var sut = new WebServerConfigurer();
            var configurer = Substitute.For<IApplicationPoolConfigurer>();


            var expected = WebAdministrationFactory.CreateApplicationPool();
            sut.ServerManager.ApplicationPools.Add(expected);

            ApplicationPool actual = default(ApplicationPool);
            WebServerConfigurer.CreateApplicationPoolConfigurer = (x) => { actual = x; return configurer; };

            sut.AlterApplicationPool(expected.Name, applicationPool => { });

            Assert.Equal(expected.Name, actual.Name);

        }


        [Fact]
        public void AlterAppPool_ThrowsWhenUnableToMatchAnAppPoolWithTheSameName()
        {
            var sut = new WebServerConfigurer();

            var randomName = Guid.NewGuid().ToString();

            Assert.Throws<InstallationException>(() => sut.AlterApplicationPool(randomName, (x) => { }));

        }
    }
}