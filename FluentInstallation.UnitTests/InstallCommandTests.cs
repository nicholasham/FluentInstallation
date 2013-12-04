using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Reflection;
using NSubstitute;
using Xunit;

namespace FluentInstallation.UnitTests
{
    public static class TestContext
    {
        
        static public string WorkingDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }

    public class InstallCommandTests
    {
        [Fact]
        public void SutIsCmdlet()
        {
            var sut = new InstallCommand(Substitute.For<IInstallerFinder>());

            Assert.IsAssignableFrom<Cmdlet>(sut);
        }

        [Fact]
        public void Invoke_ThrowsIfAssemblyFileDoesNotExist()
        {
            var sut = new InstallCommand(Substitute.For<IInstallerFinder>());

            var result = sut.Invoke().GetEnumerator();

            Assert.Throws<FileNotFoundException>(() => result.MoveNext());

        }

        [Fact]
        public void Invoke_LoadsAssemblyCorrectly()
        {
            var finder = Substitute.For<IInstallerFinder>();
            var sut = new InstallCommand(finder);


            sut.AssemblyFile = Path.Combine(@"C:\Projects\github\FluentInstallation\TestInstallerAssembly\bin\Debug\TestInstallerAssembly.dll");

            var result = sut.Invoke().GetEnumerator();
            result.MoveNext();

            finder.Received().FindInAssembly(Arg.Is<Assembly>(x => x.GetName().Name.Equals("TestInstallerAssembly")));

        }
    }
}