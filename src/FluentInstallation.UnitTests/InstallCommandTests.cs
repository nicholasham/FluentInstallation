using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Reflection;
using NSubstitute;
using Xunit;

namespace FluentInstallation.UnitTests
{
    public class InstallCommandTests
    {
        [Fact]
        public void SutIsCmdlet()
        {
            var sut = new InstallCommand(Substitute.For<IInstallerFactory>());

            Assert.IsAssignableFrom<Cmdlet>(sut);
        }

    }
}