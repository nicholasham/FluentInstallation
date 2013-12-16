using Microsoft.Web.Administration;

namespace FluentInstallation.Web
{
    public interface IServerManager
    {
        ApplicationPoolCollection ApplicationPools { get; }
        SiteCollection Sites { get; }
        void CommitChanges();
    }
}