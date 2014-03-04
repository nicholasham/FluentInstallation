using System;
using System.IO;
using System.Reflection;

namespace FluentInstallation
{
    public class AssemblyLoader : IAssemblyLoader
    {
        static AssemblyLoader()
        {
            AppDomain.CurrentDomain.AssemblyResolve += LoadFromSameFolder;
        }

        public AssemblyLoader(Func<string> getAssemblyFilePath)
        {
            if (getAssemblyFilePath == null)
            {
                throw new ArgumentNullException("getAssemblyFilePath");
            }

            GetAssemblyFilePath = getAssemblyFilePath;
        }

        private Func<string> GetAssemblyFilePath { get; set; }

        public Assembly Load()
        {
            string assemblyFilePath = GetAssemblyFilePath();

            if (!Directory.Exists(assemblyFilePath))
            {
                assemblyFilePath = Path.Combine(this.Assembly().DirectoryPath(), assemblyFilePath);
            }

            if (!File.Exists(assemblyFilePath))
            {
                throw Exceptions.AssemblyNotFound(assemblyFilePath);
            }

            return Assembly.LoadFrom(assemblyFilePath);
        }
        
        private static Assembly LoadFromSameFolder(object sender, ResolveEventArgs args)
        {
            string folderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string assemblyPath = Path.Combine(folderPath, new AssemblyName(args.Name).Name + ".dll");
            if (File.Exists(assemblyPath) == false) return null;
            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            return assembly;
        }
    }
}