namespace FluentInstallation
{
    public interface ICommand
    {
        string AssemblyFile { get;}
        void WriteWarning(string text);

        void WriteDebug(string text);

        void WriteVerbose(string text);

        void WriteCommandDetail(string text);
    }
}