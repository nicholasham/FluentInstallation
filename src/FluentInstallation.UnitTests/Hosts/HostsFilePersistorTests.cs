using System.Linq;
using Xunit;

namespace FluentInstallation.Hosts
{
    public class HostsFilePersistorTests
    {
        [Fact]
        public void Load_LoadsCorrectlyFromStream()
        {
            var sut = new HostsFilePersistor();
            var stream = TestContext.GetResourceStream("FluentInstallation.Hosts.Hosts.txt");
            
            var hostsFile = sut.Load(stream);
            
            Assert.NotEmpty(hostsFile.AllEntries());
            Assert.Equal(3, hostsFile.AllEntries().Count());
        } 
    }
}