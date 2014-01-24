using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace FluentInstallation
{
    public class AssemblyLoaderTests
    {
        [Fact]
        public void Sut_IsAssemblyLoader()
        {
            Assert.IsAssignableFrom<IAssemblyLoader>(new AssemblyLoader(()=>""));
        }

        [Fact]
        public void Load_LoadsAssemblyFromFile()
        {
            var expected = GetType().Assembly;

            var sut = new AssemblyLoader(() => expected.Location);

            var actual = sut.Load();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Load_ThrowsWhenAssemblyFileIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new AssemblyLoader(null));
        }

        [Fact]
        public void Load_ThrowsWhenAssemblyFileCanNotBeFound()
        {
            var sut = new AssemblyLoader(() => "SomeDummyAssembly.dll");
            Assert.Throws<FileNotFoundException>(() => sut.Load());
        }

        [Fact]
        public void Load_LoadsAssemblyFromExecutingDirectoryWhenOnlyTheAssemblyFileNameIsProvided()
        {

            var expected = GetType().Assembly;

            var sut = new AssemblyLoader(() => expected.GetName().Name + ".dll");

            var actual = sut.Load();

            Assert.Equal(expected, actual);
        }
    }
}