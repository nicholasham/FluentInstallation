using Xunit;

namespace FluentInstallation.Web.Hosts
{
    public class HostEntryTests
    {
        [Fact]
        public void Constructor_SetsDefaultsCorrectly()
        {
            var hostEntry = new HostEntry();
            Assert.Equal(HostEntry.LocalHostIpAddress, hostEntry.IpAddress);
            Assert.True(hostEntry.IsEnabled);
        }
        
        [Fact]
        public void TryParse_ParsesCorrectlyWhenValueIsAnUncommentedOutEntryWithNoDescription()
        {
            HostEntry hostEntry = default(HostEntry);
            var value = "127.0.0.1       mysite.co.uk";

            var actual = HostEntry.TryParse(value, out hostEntry);

            Assert.True(actual);
            Assert.Equal("127.0.0.1", hostEntry.IpAddress);
            Assert.Equal("mysite.co.uk", hostEntry.HostName);
            Assert.Equal("", hostEntry.Description);
            Assert.True(hostEntry.IsEnabled);

        }

        [Fact]
        public void TryParse_ParsesCorrectlyWhenValueIsCommentedOutEntryWithNoDescription()
        {
            HostEntry hostEntry = default(HostEntry);
            var value = "#127.0.0.1       mysite.co.uk";

            var actual = HostEntry.TryParse(value, out hostEntry);

            Assert.True(actual);
            Assert.Equal("127.0.0.1", hostEntry.IpAddress);
            Assert.Equal("mysite.co.uk", hostEntry.HostName);
            Assert.Equal("", hostEntry.Description);
            Assert.False(hostEntry.IsEnabled);


        }

        [Fact]
        public void TryParse_ParsesCorrectlyWhenValueIsAnUncommentedOutAndHasDescription()
        {
            HostEntry hostEntry = default(HostEntry);
            var value = "127.0.0.1       mysite.co.uk # This is a description";

            var actual = HostEntry.TryParse(value, out hostEntry);

            Assert.True(actual);
            Assert.Equal("127.0.0.1", hostEntry.IpAddress);
            Assert.Equal("mysite.co.uk", hostEntry.HostName);
            Assert.Equal("This is a description", hostEntry.Description);
            Assert.True(hostEntry.IsEnabled);


        }

        [Fact]
        public void TryParse_ParsesCorrectlyWhenValueIsCommentedOutAndHasDescription()
        {
            HostEntry hostEntry = default(HostEntry);
            var value = "#127.0.0.1       mysite.co.uk # This is a description";

            var actual = HostEntry.TryParse(value, out hostEntry);

            Assert.True(actual);
            Assert.Equal("127.0.0.1", hostEntry.IpAddress);
            Assert.Equal("mysite.co.uk", hostEntry.HostName);
            Assert.Equal("This is a description", hostEntry.Description);
            Assert.False(hostEntry.IsEnabled);

        }

        [Fact]
        public void ToString_ReturnsCorrectValueWhenEnabledWithNoDescription()
        {
            var hostEntry = new HostEntry();
            hostEntry.IpAddress = "127.0.0.1";
            hostEntry.IsEnabled = true;
            hostEntry.Description = null;
            hostEntry.HostName = "mysite.co.uk";

            var actual = hostEntry.ToString();
            var expected = "127.0.0.1       mysite.co.uk";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToString_ReturnsCorrectValueWhenDisabledWithNoDescription()
        {
            var hostEntry = new HostEntry();
            hostEntry.IpAddress = "127.0.0.1";
            hostEntry.IsEnabled = false;
            hostEntry.Description = null;
            hostEntry.HostName = "mysite.co.uk";

            var actual = hostEntry.ToString();
            var expected = "# 127.0.0.1       mysite.co.uk";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToString_ReturnsCorrectValueWhenEnabledWithDescription()
        {
            var hostEntry = new HostEntry();
            hostEntry.IpAddress = "127.0.0.1";
            hostEntry.IsEnabled = true;
            hostEntry.Description = "Some description";
            hostEntry.HostName = "mysite.co.uk";

            var actual = hostEntry.ToString();
            var expected = "127.0.0.1       mysite.co.uk\t# Some description";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToString_ReturnsCorrectValueWhenDisabledWithDescription()
        {
            var hostEntry = new HostEntry();
            hostEntry.IpAddress = "127.0.0.1";
            hostEntry.IsEnabled = false;
            hostEntry.Description = "Some description";
            hostEntry.HostName = "mysite.co.uk";

            var actual = hostEntry.ToString();
            var expected = "# 127.0.0.1       mysite.co.uk\t# Some description";

            Assert.Equal(expected, actual);
        }
    }
}