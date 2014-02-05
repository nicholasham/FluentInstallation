using System.IO;
using System.Linq;
using Xunit;

namespace FluentInstallation.Hosts
{
    public class HostsFileRepositoryTests
    {
        [Fact]
        public void Load_LoadsCorrectlyFromStream()
        {
            var sut = new HostsFileRepository(() => TestContext.GetResourceStream("FluentInstallation.Hosts.Hosts.txt"));
            
            var hostsFile = sut.Load();
            
            Assert.NotEmpty(hostsFile.AllEntries());
            Assert.Equal(3, hostsFile.AllEntries().Count());
            Assert.Equal("This is a comment",hostsFile.AllEntries().Last().Description);
            Assert.NotEmpty(hostsFile.AllComments());
            Assert.Equal(2, hostsFile.AllComments().Count());
        }

        public void Save_SavesCorrectlyToStream()
        {
            

            var actualStream = new MemoryStream();
            var expectedStream = TestContext.GetResourceStream("FluentInstallation.Hosts.Hosts.txt");

            var hostsFile = new HostsFile();
            hostsFile.AddComment(" This is a test comment line 1");
            hostsFile.AddComment(" This is a test comment line 2");

            hostsFile.AddEntry(new HostEntry() {IpAddress = "172.0.1.1", HostName = "mysite.co.uk", Description = string.Empty});
            hostsFile.AddEntry(new HostEntry() { IpAddress = "172.0.1.2", HostName = "mysite.de", Description = string.Empty });
            hostsFile.AddEntry(new HostEntry() { IpAddress = "172.0.1.3", HostName = "mysite.ie", Description = " This is a comment" });

            var sut = new HostsFileRepository(() => actualStream);
            sut.Save(hostsFile);

            var actual = actualStream.ReadToEnd();
            var expected = expectedStream.ReadToEnd();

            Assert.Equal(expected, actual);

        }
    }
}