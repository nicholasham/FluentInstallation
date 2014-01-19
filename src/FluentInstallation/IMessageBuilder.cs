namespace FluentInstallation
{
    public interface IMessageBuilder
    {
        IMessageBuilder Indent();
        IMessageBuilder DecreaseIndent();
        IMessageBuilder WriteLine(string text, params object[] args);


        string Build();
    }
}