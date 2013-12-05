using System;
using System.IO;

namespace FluentInstallation.UnitTests
{
    public static class TestContext
    {
        static public string OutputDirectoryPath
        {
            get
            {
                string codeBase = typeof(TestContext).Assembly.CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}