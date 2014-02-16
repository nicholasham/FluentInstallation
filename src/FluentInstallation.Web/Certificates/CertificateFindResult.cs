using System.Security.Cryptography.X509Certificates;

namespace FluentInstallation.Web.Certificates
{
    public class CertificateFindResult
    {
        public CertificateFindResult(StoreLocation storeLocation, StoreName storeName, X509Certificate2 certificate)
        {
            StoreLocation = storeLocation;
            StoreName = storeName;
            Certificate = certificate;
        }

        public CertificateFindResult()
        {
        }

        public bool Found
        {
            get { return Certificate != null; }
        }

        public StoreLocation StoreLocation { get; set; }
        public StoreName StoreName { get; set; }
        public X509Certificate2 Certificate { get; set; }

        public CertificateFindResult Succeeded(StoreLocation storeLocation, StoreName storeName,
            X509Certificate2 certificate2)
        {
            return new CertificateFindResult();
        }

    }
}