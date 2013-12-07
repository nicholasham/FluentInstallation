namespace FluentInstallation.IIS
{
    public interface IDeleteApplicationConfigurer
    {
        IWebServerConfigurer ContainedInWebsite(string name);
    }
}