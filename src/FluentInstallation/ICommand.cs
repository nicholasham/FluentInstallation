namespace FluentInstallation
{
    internal interface ICommand
    {
        string AssemblyFile { get;}

        void WriteProgress(System.Management.Automation.ProgressRecord progressRecord);
        void WriteWarning(string text);

        void WriteDebug(string text);

        void WriteVerbose(string text);

        void WriteCommandDetail(string text);
    }
}