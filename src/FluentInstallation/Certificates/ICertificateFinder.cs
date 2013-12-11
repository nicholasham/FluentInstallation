using System.Security.Cryptography.X509Certificates;

namespace FluentInstallation.Certificates
{
    public interface ICertificateFinder
    {
        CertificateFindResult Find(X509FindType findType, object findValue);
    }
}