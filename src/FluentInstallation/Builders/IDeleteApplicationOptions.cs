namespace FluentInstallation.Builders
{
    public interface IDeleteApplicationOptions
    {
        IIisConfigurationOptions ContainedInWebsite(string name);
    }
}