using Xunit;

namespace FluentInstallation.IIS
{
    public class BindingInformationTests
    {
        [Fact]
        public void Constructor_ShouldSetDefaults()
        {
            var information = new BindingInformation();

            Assert.Equal("*", information.IpAddress);
            Assert.Equal(80, information.Port);
            Assert.Equal(string.Empty, information.HostName);

        }

        [Fact]
        public void Parse_ConstructsTheBindingCorrectly()
        {
            var information = BindingInformation.Parse("192.168.1.1:8080:mysite.com");

            Assert.Equal("192.168.1.1", information.IpAddress);
            Assert.Equal(8080, information.Port);
            Assert.Equal("mysite.com", information.HostName);
        }

        [Fact]
        public void Parse_ReturnsDefaultsWhenPassedEmptyString()
        {
            var information = BindingInformation.Parse(string.Empty);

            Assert.Equal("*", information.IpAddress);
            Assert.Equal(80, information.Port);
            Assert.Equal(string.Empty, information.HostName);
        }

        [Fact]
        public void ToString_BuildsTheCorrectStringRepresentingTheBinding()
        {
            var information = new BindingInformation();
            information.Port = 8080;
            information.IpAddress = "192.168.1.1";
            information.HostName = "mysite.com";

            var expected = "192.168.1.1:8080:mysite.com";
            var actual = information.ToString();

            Assert.Equal(expected, actual);
        }
    }
}