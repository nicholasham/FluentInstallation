using System.IO;
using System.Linq;
using Xunit;

namespace FluentInstallation.Web.Hosts
{
    public class HostsFileTests
    {
        [Fact]
        public void Constructor_InitialisesCorrectly()
        {
            var sut = new HostsFile();

            Assert.Empty(sut.AllComments());
            Assert.Empty(sut.AllHostEntries());
        }

        [Fact]
        public void AddHostEntry_AddsEntryToHostFile()
        {
            var sut = new HostsFile();
            sut.AddHostEntry(new HostEntry());
            Assert.Equal(1, sut.AllHostEntries().Count());
        }


        [Fact]
        public void AddHostEntry_ShouldDisableDuplicateEntriesWithTheSameHostName()
        {
            var sut = new HostsFile();
            var hostEntry1 = new HostEntry() { HostName = "mysite.co.uk", IsEnabled = true, IpAddress = "192.168.2.1" };
            var hostEntry2 = new HostEntry() { HostName = "mysite.co.uk", IsEnabled = true, IpAddress = "192.168.2.3" };

            sut.AddHostEntry(hostEntry1);
            sut.AddHostEntry(hostEntry2);


            Assert.Equal(2, sut.AllHostEntries().Count());
            Assert.False(sut.AllHostEntries().First().IsEnabled);
            Assert.True(sut.AllHostEntries().Last().IsEnabled);

        }

        [Fact]
        public void AddHostEntry_ShouldNotAddDuplicateEntriesWithTheSameHostNameAndIp()
        {
            var sut = new HostsFile();
            var hostEntry1 = new HostEntry() { HostName = "mysite.co.uk", IsEnabled = true, IpAddress = "192.168.2.1"};
            var hostEntry2 = new HostEntry() { HostName = "mysite.co.uk", IsEnabled = true, IpAddress = "192.168.2.1" };
            
            sut.AddHostEntry(hostEntry1);
            sut.AddHostEntry(hostEntry2);
            
            Assert.Equal(1, sut.AllHostEntries().Count());
            Assert.True(sut.AllHostEntries().Last().IsEnabled);

        }

        [Fact]
        public void AddComment_AddsCommentToHostFile()
        {
            var sut = new HostsFile();
            sut.AddComment("Test comment");
            Assert.Equal(1, sut.AllComments().Count());
        }

        [Fact]
        public void RemoveHostEntry_RemovesEntryFromHostFile()
        {
            var sut = new HostsFile();
            var entry = new HostEntry(){HostName = "mysite.co.uk"};
            sut.AddHostEntry(entry);

            sut.RemoveHostEntry(entry);

            Assert.Empty(sut.AllHostEntries());

        }

        [Fact]
        public void RemoveComment_RemovesCommentFromHostFile()
        {
            var sut = new HostsFile();
            sut.AddComment("Test comment");

            sut.RemoveComment("Test comment");

            Assert.Empty(sut.AllComments());
        }

        [Fact]
        public void Load_LoadsHostsFileFromAStream()
        {
            var hostsFile = HostsFile.Load(TestContext.GetResourceStream("FluentInstallation.Web.Hosts.Hosts.txt"));

            Assert.NotEmpty(hostsFile.AllHostEntries());
            Assert.Equal(3, hostsFile.AllHostEntries().Count());
            Assert.Equal("This is a comment", hostsFile.AllHostEntries().Last().Description);
            Assert.NotEmpty(hostsFile.AllComments());
            Assert.Equal(2, hostsFile.AllComments().Count());
        }

        [Fact]
        public void Save_SavesHostFileToStream()
        {


            var actualStream = new MemoryStream();
            var expectedStream = TestContext.GetResourceStream("FluentInstallation.Web.Hosts.Hosts.txt");

            var hostsFile = new HostsFile();
            hostsFile.AddComment("This is a test comment line 1");
            hostsFile.AddComment("This is a test comment line 2");

            hostsFile.AddHostEntry(new HostEntry() { IpAddress = "172.0.1.1", HostName = "mysite.co.uk", Description = string.Empty, IsEnabled = true });
            hostsFile.AddHostEntry(new HostEntry() { IpAddress = "172.0.1.2", HostName = "mysite.de", Description = string.Empty, IsEnabled = true });
            hostsFile.AddHostEntry(new HostEntry() { IpAddress = "172.0.1.3", HostName = "mysite.ie", Description = "This is a comment", IsEnabled = true });

            hostsFile.Save(actualStream);

            actualStream.Position = 0;
            var actual = actualStream.ReadToEnd();
            var expected = expectedStream.ReadToEnd();

            Assert.Equal(expected, actual);

        }
    }
}