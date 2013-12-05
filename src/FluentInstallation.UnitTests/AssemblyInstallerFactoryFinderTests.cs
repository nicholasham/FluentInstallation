using System.IO;
using System.Linq;
using NSubstitute;
using Xunit;

namespace FluentInstallation.UnitTests
{
    public class AssemblyInstallerFactoryFinderTests
    {
        [Fact]
        public void SutIsIInstallerFactoryFinder()
        {
            var sut = new AssemblyInstallerFactoryFinder(Substitute.For<ICommand>());
            Assert.IsAssignableFrom<IInstallerFactoryFinder>(sut);
        }

        [Fact]
        public void Create_ThrowsWhenAssemblyFileDoesNotExist()
        {
            var context = Substitute.For<ICommand>();
            context.AssemblyFile.Returns("");

            var sut = new AssemblyInstallerFactoryFinder(context);

            Assert.Throws<FileNotFoundException>(() => sut.Find());

        }

        [Fact]
        public void Create_ThrowsWhenResolverNotFoundInAssembly()
        {
            var context = Substitute.For<ICommand>();
            var assemblyFile = Path.Combine(TestContext.OutputDirectoryPath, "FluentInstallation.TestEmptyAssembly.dll");
            context.AssemblyFile.Returns(assemblyFile);

            var sut = new AssemblyInstallerFactoryFinder(context);

            Assert.Throws<InstallationException>(() => sut.Find());

        }

        [Fact]
        public void Create_LoadsInstallerResolverCorrectly()
        {
            var context = Substitute.For<ICommand>();
            var assemblyFile = Path.Combine(TestContext.OutputDirectoryPath, "FluentInstallation.TestAssembly.dll");
            context.AssemblyFile.Returns(assemblyFile);

            var sut = new AssemblyInstallerFactoryFinder(context);

            var actual = sut.Find();
            
            Assert.IsAssignableFrom<IInstallerFactory>(actual);
        }

    }
}