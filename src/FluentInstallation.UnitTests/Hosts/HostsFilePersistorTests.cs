using System.IO;
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
            Assert.Equal("This is a comment",hostsFile.AllEntries().Last().Comment);
            Assert.NotEmpty(hostsFile.AllComments());
            Assert.Equal(2, hostsFile.AllComments().Count());
        }

        public void Save_SavesCorrectlyToStream()
        {
            var sut = new HostsFilePersistor();

            var actualStream = new MemoryStream();
            var expectedStream = TestContext.GetResourceStream("FluentInstallation.Hosts.Hosts.txt");

            var hostsFile = new HostsFile();
            hostsFile.AddComment(" This is a test comment line 1");
            hostsFile.AddComment(" This is a test comment line 2");

            hostsFile.AddEntry(new HostEntry() {Ip = "172.0.1.1", HostName = "mysite.co.uk", Comment = string.Empty});
            hostsFile.AddEntry(new HostEntry() { Ip = "172.0.1.2", HostName = "mysite.de", Comment = string.Empty });
            hostsFile.AddEntry(new HostEntry() { Ip = "172.0.1.3", HostName = "mysite.ie", Comment = " This is a comment" });

            sut.Save(actualStream, hostsFile);

            var actual = actualStream.ReadToEnd();
            var expected = expectedStream.ReadToEnd();

            Assert.Equal(expected, actual);

        }
    }
}