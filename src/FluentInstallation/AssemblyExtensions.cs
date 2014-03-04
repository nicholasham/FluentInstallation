using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public static string ParentDirectoryPath( this Assembly assembly)
        {
            return Directory.GetParent(assembly.DirectoryPath()).FullName;
        }

        public static IEnumerable<Type> FindTypesImplementing<T>(this Assembly assembly)
        {
            return assembly.GetLoadableTypes().Where(type => typeof(T).IsAssignableFrom(type));
        }

        public static IEnumerable<Type> FindInstallerTypes(this Assembly assembly)
        {
            return assembly.FindTypesImplementing<IInstaller>();
        }


        public static IEnumerable<Type> FindInstallerTypesMarkedAsDefault(this Assembly assembly)
        {
            return assembly.FindInstallerTypes().Where(type => type.GetCustomAttributes<DefaultInstallerAttribute>().Any());
        }

        public static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
        {
            
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null && t.Assembly == assembly);
            }
        }

    }
}