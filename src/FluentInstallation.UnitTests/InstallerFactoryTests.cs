using System;
using System.Linq;
using FluentInstallation.TestNoDefaultInstallerAssembly;
using Xunit;

namespace FluentInstallation
{
    public class InstallerFactoryTests
    {
        [Fact]
        public void Constructor_ConstructsAnInstallerFactory()
        {
            var sut = new InstallerFactory(() => GetType().Assembly);
            Assert.IsAssignableFrom<IInstallerFactory>(sut);
        }

        [Fact]
        public void Constructor_ThrowsWhenAssemblyIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new InstallerFactory(null));
        }

        [Fact]
        public void Create_ReturnsTheFirstInstallerInTheAssemblyThatIsMarkedAsTheDefaultInstaller()
        {
            var sut = new InstallerFactory(() => GetType().Assembly);
            var result = sut.Create();

            Assert.IsType<TestInstaller1>(result);
        }

        [Fact]
        public void Create_ThrowsWhenUnableToFindADefaultInstallerInAnAssembly()
        {
            var sut = new InstallerFactory(() => typeof(MyInstaller).Assembly);
            Assert.Throws<InvalidOperationException>(() => sut.Create());
        }

        [Fact]
        public void Create_ReturnsTheFirstInstallerThatHasTheSameTypeNameAsNamePassedAsAnArgument()
        {
            var sut = new InstallerFactory(() => GetType().Assembly);
            var result = sut.Create(typeof(TestInstaller2).Name);

            Assert.IsType<TestInstaller2>(result);
        }

        [Fact]
        public void Create_ThrowsWhenTypeNameArgumentIsNull()
        {
            var sut = new InstallerFactory(() => GetType().Assembly);
            Assert.Throws<ArgumentNullException>(() => sut.Create(null));
        }


    }
}