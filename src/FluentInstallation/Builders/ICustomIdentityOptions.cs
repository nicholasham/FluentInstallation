namespace FluentInstallation.Builders
{
    public interface ICustomIdentityOptions : IFluentSyntax
    {
        ICustomIdentityOptions WithUsername(string userName);
        ICustomIdentityOptions WithPassword(string userName);
    }
}