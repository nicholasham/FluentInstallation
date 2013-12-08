using System;
using System.IO;

namespace FluentInstallation
{
    public static class TestContext
    {
        static public string OutputDirectoryPath
        {
            get
            {
                string codeBase = typeof(TestContext).Assembly.CodeBase;
                var uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}