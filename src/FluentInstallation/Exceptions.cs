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

        public static Exception NoCertificateFoundMatchingThumbprint(string thumbprint)
        {
            return new InstallationException(Format("Unable to find a certificate on machine {0} with a certificate matching thumbprint {1}", Environment.MachineName, thumbprint));
        }

        static string Format(string format, params object[] args)
        {
            return  string.Format(format, args);
        }
    }
}