using System;
using System.IO;
using System.Reflection;

namespace FluentInstallation
{
    public static class Exceptions
    {
        public static Exception AssemblyNotFound(string assemblyFileName)
        {
            return new FileNotFoundException(Format("Assembly not found on path {0}", assemblyFileName));
        }

        public static Exception InstallerFactoryNotFoundInAssembly(Assembly assembly)
        {
            return new InstallationException(Format("Unable to find any type implementing IInstallerFactory in Assembly {0}. Did you forget to implement the interface?", assembly.FullName));
        }

        static string Format(string format, params object[] args)
        {
            return  string.Format(format, args);
        }
    }
}