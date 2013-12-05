using System.IO;
using System.Linq;
using NSubstitute;
using Xunit;

namespace FluentInstallation.UnitTests
{
    public class AssemblyInstallerFactoryLoaderTest
    {
        [Fact]
        public void SutIsInstallerFactoryLoader()
        {
            var sut = new AssemblyInstallerFactoryLoader(Substitute.For<IAssemblyContext>());
            Assert.IsAssignableFrom<IInstallerFactoryLoader>(sut);
        }

        [Fact]
        public void FindThrowsWhenAssemblyFileDoesNotExist()
        {
            var context = Substitute.For<IAssemblyContext>();
            context.AssemblyFile.Returns("");

            var sut = new AssemblyInstallerFactoryLoader(context);

            Assert.Throws<FileNotFoundException>(() => sut.Load());

        }

        [Fact]
        public void FindThrowsWhenFactoryNotFoundInAssembly()
        {
            var context = Substitute.For<IAssemblyContext>();
            var assemblyFile = Path.Combine(TestContext.OutputDirectoryPath, "FluentInstallation.TestEmptyAssembly.dll");
            context.AssemblyFile.Returns(assemblyFile);

            var sut = new AssemblyInstallerFactoryLoader(context);

            Assert.Throws<InstallationException>(() => sut.Load());

        }

        [Fact]
        public void FindLoadsInstallerFactoryCorrectly()
        {
            var context = Substitute.For<IAssemblyContext>();
            var assemblyFile = Path.Combine(TestContext.OutputDirectoryPath, "FluentInstallation.TestAssembly.dll");
            context.AssemblyFile.Returns(assemblyFile);

            var sut = new AssemblyInstallerFactoryLoader(context);

            var actual = sut.Load();
            
            Assert.IsAssignableFrom<IInstallerFactory>(actual);
        }

    }
}