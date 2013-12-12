using Microsoft.Web.Administration;

namespace FluentInstallation.IIS
{
    public interface IServerManager
    {
        ApplicationPoolCollection ApplicationPools { get; }
        SiteCollection Sites { get; }
        void CommitChanges();
    }
}