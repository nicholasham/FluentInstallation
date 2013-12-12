using System.IO;
using NSubstitute;
using Xunit;

namespace FluentInstallation
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
        public void Create_ThrowsWhenFactoryNotFoundInAssembly()
        {
            var context = Substitute.For<ICommand>();
            var assemblyFile = Path.Combine(TestContext.OutputDirectoryPath, "FluentInstallation.TestEmptyAssembly.dll");
            context.AssemblyFile.Returns(assemblyFile);

            var sut = new AssemblyInstallerFactoryFinder(context);

            Assert.Throws<InstallationException>(() => sut.Find());

        }

        [Fact]
        public void Create_LoadsFactoryCorrectlyWhenGivenFullPathToAssembly()
        {
            var context = Substitute.For<ICommand>();
            var assemblyFile = Path.Combine(TestContext.OutputDirectoryPath, "FluentInstallation.TestAssembly.dll");
            context.AssemblyFile.Returns(assemblyFile);

            var sut = new AssemblyInstallerFactoryFinder(context);

            var actual = sut.Find();
            
            Assert.IsAssignableFrom<IInstallerFactory>(actual);
        }


        /// <summary>
        /// NCrunch Note: Set copy reference assemblies to workspace to true otherwise this test will fail
        /// </summary>

        [Fact]
        public void Create_LoadsFactoryCorrectlyWhenGivenAssemblyFileNameAndAssemblyIsInSameDirectory()
        {
            var context = Substitute.For<ICommand>();
            var assemblyFile = "FluentInstallation.TestAssembly.dll";
            context.AssemblyFile.Returns(assemblyFile);

            var sut = new AssemblyInstallerFactoryFinder(context);

            var actual = sut.Find();

            Assert.IsAssignableFrom<IInstallerFactory>(actual);
        }

    }
}