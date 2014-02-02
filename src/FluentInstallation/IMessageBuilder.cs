namespace FluentInstallation
{
    public interface IMessageBuilder
    {
        IMessageBuilder IncreaseIndent();
        IMessageBuilder DecreaseIndent();
        IMessageBuilder WriteLine(string text, params object[] args);


        string Build();
    }
}