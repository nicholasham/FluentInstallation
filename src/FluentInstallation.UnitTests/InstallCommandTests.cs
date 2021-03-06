﻿using System.Management.Automation;
using NSubstitute;
using Xunit;

namespace FluentInstallation
{
    public class InstallCommandTests
    {
        [Fact]
        public void SutIsCmdlet()
        {
            var sut = new InstallCommand(Substitute.For<IInstallerFactory>());

            Assert.IsAssignableFrom<Cmdlet>(sut);
        }

        [Fact]
        public void Invoke_CallsInstallOnAllInstallers()
        {
            var installerFactory = Substitute.For<IInstallerFactory>();

            var installer1 = Substitute.For<IInstaller>();

            installerFactory.Create().Returns(installer1);

            var sut = new InstallCommand(installerFactory);

            sut.Invoke().GetEnumerator().MoveNext();

            installer1.Received().Install(Arg.Is<IInstallerContext>(context => context is InstallerContext));

        }

        [Fact]
        public void DefaultConstructor_SetsDefaults()
        {
            var sut = new InstallCommand();
            Assert.NotNull(sut.Parameters);
        }

    }
}