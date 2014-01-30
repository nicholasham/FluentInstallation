using Microsoft.Web.Administration;

namespace FluentInstallation.WebAdministration
{
    public interface IServerManager
    {
        ApplicationPoolCollection ApplicationPools { get; }
        SiteCollection Sites { get; }
        bool SiteExists(string name);
        bool ApplicationPoolExists(string name);
        void CommitChanges();
    }
}