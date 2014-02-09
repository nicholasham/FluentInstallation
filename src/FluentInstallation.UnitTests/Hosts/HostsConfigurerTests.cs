using System;
using System.IO;
using System.Linq;
using Xunit;

namespace FluentInstallation.Hosts
{
    public class HostsConfigurerTests
    {
        [Fact]
        public void AddHostEntry_ChainsCorrectly()
        {
            var sut = CreateSut();
            var actual = sut.AddHostEntry(hostEntry => { });

            Assert.Equal(sut, actual);
        }

        private HostsConfigurer CreateSut()
        {
            return new HostsConfigurer(() => TestContext.GetResourceStream("FluentInstallation.Hosts.Hosts.txt"));
        }

        [Fact]
        public void AddHostEntry_PassesNewHostEntryToConfigurer()
        {
            var sut = CreateSut();

            bool actual = false;

            Action<IHostEntryConfigurer> action = (options) => { actual = options != null; };

            sut.AddHostEntry(action);

            Assert.True(actual);
        }

        [Fact]
        public void AddHostEntry_AddsNewHostEntryToHostsFile()
        {
            var sut = CreateSut();

            int expected = sut.HostsFile.AllHostEntries().Count() + 1;

            sut.AddHostEntry((options) => { });

            int actual = sut.HostsFile.AllHostEntries().Count();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RemoveHostEntry_RemovesHostEntryFromHostsFile()
        {
            var sut = CreateSut();

            var expected = sut.HostsFile.AllHostEntries().Count();
            
            sut.HostsFile.AddHostEntry(new HostEntry() {HostName = "Mytestentry"});

            sut.RemoveHostEntry("Mytestentry");

            Assert.Equal(expected, sut.HostsFile.AllHostEntries().Count());

        }
    }
}