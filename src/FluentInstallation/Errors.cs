using System;
using System.IO;

namespace FluentInstallation
{
    public static class Errors
    {
        public static Exception AssemblyNotFound(string assemblyFileName)
        {
            return new FileNotFoundException(Format("Assembly not found on path {0}", assemblyFileName));
        }

        static string Format(string format, params object[] args)
        {
            return  string.Format(format, args);
        }
    }
}