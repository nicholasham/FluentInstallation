using System;
using System.IO;
using System.Reflection;

namespace FluentInstallation
{
    public class AssemblyLoader : IAssemblyLoader
    {
        private Func<string> GetAssemblyFilePath { get; set; }
        
        public AssemblyLoader(Func<string> getAssemblyFilePath)
        {
            if (getAssemblyFilePath == null)
            {
                throw new ArgumentNullException("getAssemblyFilePath");
            }

            GetAssemblyFilePath = getAssemblyFilePath;
        }

        public Assembly Load()
        {
            var assemblyFilePath = GetAssemblyFilePath();

            if (!Directory.Exists(assemblyFilePath))
            {
              assemblyFilePath =  Path.Combine(this.Assembly().DirectoryPath(), assemblyFilePath);
            }

            if (!File.Exists(assemblyFilePath))
            {
               throw Exceptions.AssemblyNotFound(assemblyFilePath);
            }
            
            return Assembly.LoadFrom(assemblyFilePath);
        }

    }
}