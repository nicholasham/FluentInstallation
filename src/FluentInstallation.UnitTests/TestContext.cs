using System;
using System.IO;
using System.Linq;
using NSubstitute.Exceptions;

namespace FluentInstallation
{
    public static class TestContext
    {
        public static string OutputDirectoryPath
        {
            get
            {
                string codeBase = typeof(TestContext).Assembly.CodeBase;
                var uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        public static byte[] GetResourceBytes(string resourcName)
        {
            using (var stream = GetResourceStream(resourcName))
            {
                using (var reader = new BinaryReader(stream))
                {
                    return reader.ReadBytes((int) stream.Length);
                }
            }
        }

        public static string ReadToEnd(this Stream stream)
        {
            stream.Position = 0;
            return new StreamReader(stream).ReadToEnd();
        }


        public static Stream GetResourceStream(string resourceName)
        {
            var assembly = typeof (TestContext).Assembly;
            var fullName =
                assembly.GetManifestResourceNames()
                    .FirstOrDefault(x => x.EndsWith(resourceName, StringComparison.InvariantCultureIgnoreCase));

            if (string.IsNullOrEmpty(fullName))
            {
                throw new ArgumentNotFoundException(string.Format("unable to find test resource {0} in test assembly.", resourceName));
            }

            return assembly.GetManifestResourceStream(fullName);
        }
    }
}