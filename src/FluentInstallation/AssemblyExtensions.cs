using System.IO;
using System.Reflection;

namespace FluentInstallation
{

    public class PhysicalPath
    {
        public static PhysicalPath Relative(PhysicalPath physicalPath)
        {
            return new PhysicalPath();        
        }

        public static PhysicalPath ToThisAssembly()
        {
            return new PhysicalPath();
        }

        public static PhysicalPath Get()
        {
            return new PhysicalPath();
        }


        public PhysicalPath Combine(PhysicalPath path)
        {
            return new PhysicalPath();
        }
        
    }
    
    public static class This
    {
        public static Assembly Assembly()
        {
            return typeof (This).Assembly;
        }
        
    }

    public static class AssemblyExtensions
    {
        public static DirectoryInfo Directory(this Assembly assembly)
        {
            return new DirectoryInfo(Assembly.GetExecutingAssembly().Location);
        }
    }
}