namespace FluentInstallation.IIS
{
    public interface ICustomIdentityOptions : IFluentSyntax
    {
        ICustomIdentityOptions WithUsername(string userName);
        ICustomIdentityOptions WithPassword(string userName);
    }
}