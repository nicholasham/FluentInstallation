using System.Security.Cryptography.X509Certificates;

namespace FluentInstallation.Web.Certificates
{
    public interface ICertificateFinder
    {
        CertificateFindResult Find(X509FindType findType, object findValue);
    }
}