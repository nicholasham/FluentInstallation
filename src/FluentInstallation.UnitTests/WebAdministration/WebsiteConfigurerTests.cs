using System;
using System.IO;
using System.Linq;
using Microsoft.Web.Administration;
using Xunit;

namespace FluentInstallation.WebAdministration
{
    public class WebsiteConfigurerTests
    {
        [Fact]
        public void SutIsWebsiteConfigurer()
        {
            var website = WebAdministrationFactory.CreateWebsite();
            var sut = new WebsiteConfigurer(website);
            Assert.IsAssignableFrom<IWebsiteConfigurer>(sut);
        }
        
        [Fact]
        public void Constructor_ThrowsWhenWebsiteIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new WebsiteConfigurer(null));
        }

        [Fact]
        public void Configure_GivesDirectAccessToTheWebsite()
        {
            var expected = WebAdministrationFactory.CreateWebsite();
            var sut = new WebsiteConfigurer(expected);
            var actual = default(Site);

            sut.Configure(x => actual = x);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Named_SetsTheName()
        {
            var website = WebAdministrationFactory.CreateWebsite();
            var sut = new WebsiteConfigurer(website);

            sut.Named("TestWebsite");

            Assert.Equal("TestWebsite", website.Name);
        }

        [Fact]
        public void Named_ThrowsWhenNull()
        {
            var website = WebAdministrationFactory.CreateWebsite();
            var sut = new WebsiteConfigurer(website);

            Assert.Throws<ArgumentNullException>(() => sut.Named(null));
        }


        [Fact]
        public void UseApplicationPool_SetsTheApplicationPoolCorrectly()
        {
            var website = WebAdministrationFactory.CreateWebsite();
            var sut = new WebsiteConfigurer(website);
            
            sut.UseApplicationPool("TestApplicationPool");

            Assert.Equal("TestApplicationPool", website.Applications[0].ApplicationPoolName);

        }

        [Fact]
        public void OnPhysicalPath_SetsThePathCorrectly()
        {
            var website = WebAdministrationFactory.CreateWebsite();
            var sut = new WebsiteConfigurer(website);
            
            var expected = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            
            sut.OnPhysicalPath(expected);
            
            var actual = website.Applications.First().VirtualDirectories.First().PhysicalPath;

            Assert.Equal(expected, actual);
        }


        [Fact]
        public void AddBinding_PassesBindingToConfigurer()
        {
            var website = WebAdministrationFactory.CreateWebsite();
            var sut = new WebsiteConfigurer(website);
            var actual = false;

            Action<IBindingConfigurer> action = (binding) =>
            {
                actual = binding != null;
            };

            sut.AddBinding(action);

            Assert.True(actual);

        }

        [Fact]
        public void AddBinding_AddsANewBindingToTheSite()
        {
            var website = WebAdministrationFactory.CreateWebsite();
            var sut = new WebsiteConfigurer(website);

            website.Bindings.Clear();

            sut.AddBinding((binding) => { binding.OnPort(81); });

            var actual = website.Bindings.Count;
            
            Assert.Equal(1, actual);

        }

        [Fact]
        public void AddApplication_PassesApplicationToConfigurer()
        {
            var website = WebAdministrationFactory.CreateWebsite();
            var sut = new WebsiteConfigurer(website);
            var actual = false;

            Action<IApplicationConfigurer> action = (configurer) =>
            {
                actual = configurer != null;
            };

            sut.AddApplication(action);

            Assert.True(actual);

        }

        [Fact]
        public void AddApplication_AddsANewApplicationToTheSite()
        {
            var website = WebAdministrationFactory.CreateWebsite();
            var sut = new WebsiteConfigurer(website);

            sut.AddApplication((application) => { });

            var actual = website.Applications.Count;

            Assert.Equal(2, actual);

        }


        [Fact]
        public void AddVirtualDirectory_PassesNewVirtualDirectoryToConfigurer()
        {
            var website = WebAdministrationFactory.CreateWebsite();
            var sut = new WebsiteConfigurer(website);
            var actual = false;

            Action<IVirtualDirectoryConfigurer> action = (configurer) =>
            {
                actual = configurer != null;
            };

            sut.AddVirtualDirectory(action);

            Assert.True(actual);

        }

        [Fact]
        public void AddVirtualDirectory_AddsANewVirtualDirectoryToTheSite()
        {
            var website = WebAdministrationFactory.CreateWebsite();
            var sut = new WebsiteConfigurer(website);

            sut.AddVirtualDirectory((application) => { });

            var actual = website.Applications.Count;

            Assert.Equal(1, actual);

        }
        
        [Fact]
        public void RemoveApplication_RemovesApplicationFromSite()
        {
            var website = WebAdministrationFactory.CreateWebsite();
            website.Applications.Add("/SomeAlias", @"C:\");

            var sut = new WebsiteConfigurer(website);

            sut.RemoveApplication("SomeAlias");

            Assert.Equal(0, website.Applications.Count(application => application.Path.Equals("/SomeAlias")));

        }


        [Fact]
        public void RemoveVirtualDirectory_ThrowsWhenApplicationDoesNotExist()
        {
            var website = WebAdministrationFactory.CreateWebsite();
            var sut = new WebsiteConfigurer(website);

            Assert.Throws<InstallationException>(() => { sut.RemoveVirtualDirectory("somemissingapplication"); });

        }


        [Fact]
        public void RemoveVirtualDirectory_RemovesVirtualDirectoryFromSite()
        {

            var website = WebAdministrationFactory.CreateWebsite();
            website.Application().VirtualDirectories.Add("/SomeAlias", @"C:\");

            var sut = new WebsiteConfigurer(website);

            sut.RemoveVirtualDirectory("SomeAlias");

            Assert.Equal(0, website.Application().VirtualDirectories.Count(x => x.Path.Equals("/SomeAlias")));

        }

        [Fact]
        public void AssertApplicationExists_ThrowsWhenApplicationDoesNotExistWithAlias()
        {
            var website = WebAdministrationFactory.CreateWebsite();

            var sut = new WebsiteConfigurer(website);

            Assert.Throws<InstallationException>(() => sut.AssertApplicationExists("some alias"));
        }


        [Fact]
        public void AssertVirtualDirectoryExists_ThrowsWhenVirtualDirectoryDoesNotExistWithAlias()
        {
            var website = WebAdministrationFactory.CreateWebsite();

            var sut = new WebsiteConfigurer(website);

            Assert.Throws<InstallationException>(() => sut.AssertVirtualDirectoryExists("some alias"));
        }


        [Fact]
        public void WithId_SetsIdCorrectly()
        {
            var website = WebAdministrationFactory.CreateWebsite();
            var sut = new WebsiteConfigurer(website);

            sut.WithId(99999);

            Assert.Equal(99999, website.Id);
        }

        [Fact]
        public void WithId_ThrowsWhenTheIdIsAlreadyInUse()
        {
            var website = WebAdministrationFactory.CreateWebsite();
            var sut = new WebsiteConfigurer(website);

            sut.WithId(99999);

            Assert.Equal(99999, website.Id);
        }
                
    }
}