namespace FluentInstallation.IIS
{
    public interface IDeleteApplicationOptions
    {
        IWebServerConfigurer ContainedInWebsite(string name);
    }
}