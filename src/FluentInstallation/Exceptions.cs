using System;
using System.IO;
using System.Reflection;

namespace FluentInstallation
{
    public static class Exceptions
    {
        public static Exception PhysicalPathDoesNotExist(string path)
        {
            return new DirectoryNotFoundException(Message("Unable to find any directory on path {0}", path));
        }

        public static Exception AssemblyNotFound(string assemblyFileName)
        {
            return new FileNotFoundException(Message("Assembly not found on path {0}", assemblyFileName));
        }

        public static Exception InstallerFactoryNotFoundInAssembly(Assembly assembly)
        {
            return
                new InstallationException(
                    Message(
                        "Unable to find any type implementing IInstallerFactory in Assembly {0}. Did you forget to implement the interface?",
                        assembly.FullName));
        }

        private static string Message(string format, params object[] args)
        {
            return string.Format(format, args);
        }
    }
}