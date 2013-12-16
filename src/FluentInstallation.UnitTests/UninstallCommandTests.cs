using System.Management.Automation;
using NSubstitute;
using Xunit;

namespace FluentInstallation
{
    public class UninstallCommandTests
    {
        [Fact]
        public void SutIsCmdlet()
        {
            var sut = new InstallCommand(Substitute.For<IInstallerFactoryFinder>());

            Assert.IsAssignableFrom<Cmdlet>(sut);
        }

        [Fact]
        public void Invoke_CallsUninstallOnAllInstallers()
        {
            var factoryLoader = Substitute.For<IInstallerFactoryFinder>();
            var installerFactory = Substitute.For<IInstallerFactory>();

            var installer1 = Substitute.For<IInstaller>();
            var installer2 = Substitute.For<IInstaller>();

            factoryLoader.Find().Returns(installerFactory);
            installerFactory.Create().Returns(new[] { installer1, installer2 });

            var sut = new UninstallCommand(factoryLoader);

            sut.Invoke().GetEnumerator().MoveNext();

            installer1.Received().Uninstall(Arg.Is<IInstallerContext>(context => context is InstallerContext));
            installer2.Received().Uninstall(Arg.Is<IInstallerContext>(context => context is InstallerContext));

        }

        [Fact]
        public void DefaultConstructor_SetsDefaults()
        {
            var sut = new InstallCommand();
            Assert.NotNull(sut.Parameters);
        }

    }
}