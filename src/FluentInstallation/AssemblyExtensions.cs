using System;
using System.IO;
using System.Reflection;

namespace FluentInstallation
{

    public static class AssemblyExtensions
    {
        public static Assembly Assembly(this object instance)
        {
            return typeof (AssemblyExtensions).Assembly;
        }

        public static string DirectoryPath( this Assembly assembly)
        {
            return Path.GetDirectoryName(assembly.Location);
        }
    }
}