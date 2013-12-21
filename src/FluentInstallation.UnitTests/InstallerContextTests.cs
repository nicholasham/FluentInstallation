using System;
using System.Collections;
using NSubstitute;
using Xunit;

namespace FluentInstallation
{
    public class InstallerContextTests
    {
        public class SingleStringParameter
        {
            public string SiteName { get; set; }
        }

        [Fact]
        public void SutIsIInstallerContext()
        {
            var logger = Substitute.For<ILogger>();
            var parameters = new Hashtable();

            Assert.IsAssignableFrom<InstallerContext>(new InstallerContext(parameters, logger));
            
        }

        [Fact]
        public void Contructor_ThrowsWhenParametersIsNull()
        {
            var logger = Substitute.For<ILogger>();
            Assert.Throws<ArgumentNullException>(() => new InstallerContext(null, logger));
        }

        [Fact]
        public void Contructor_ThrowsWhenLoggerIsNull()
        {
            var parameters = new Hashtable();

            Assert.Throws<ArgumentNullException>(() => new InstallerContext(parameters, null));
        }

    }
}
