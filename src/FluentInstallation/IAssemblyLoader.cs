using System.Reflection;

namespace FluentInstallation
{
    public interface IAssemblyLoader
    {
        Assembly Load();
    }
}