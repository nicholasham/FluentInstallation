using System.Collections;

namespace FluentInstallation
{
    public interface ICommand
    {
        string AssemblyFile { get;}

        Hashtable Parameters { get;  }

        void WriteWarning(string text);

        void WriteDebug(string text);

        void WriteVerbose(string text);

        void WriteCommandDetail(string text);
    }
}