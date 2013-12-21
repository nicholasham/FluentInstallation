using System;
using System.Collections;
using NSubstitute;

namespace FluentInstallation.IntegrationTests
{

    public class TestInstallerContext : InstallerContext
    {
        public TestInstallerContext(IDictionary parameters)
            : base(parameters, Substitute.For<ILogger>())
        {

        }

    }
}