using System.Security.Cryptography.X509Certificates;
using FluentInstallation.Web.Administration;
using Xunit;

namespace FluentInstallation.Certificates
{
    public class CertificateFinderTests : DisposableTestFixture
    {
        
        private X509Certificate2 CreateCertificateIn(StoreName storeName, StoreLocation storeLocation)
        {
            var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadWrite);
            var certificate = CertificateFactory.CreateCertificate();

            store.Add(certificate);

            RegisterDisposable(() =>
            {
                store.Remove(certificate);
                store.Close();
            });

            return certificate;
        }
        
        [Fact]
        public void Find_ReturnsNotFoundWhenCertificateDoesNotExist()
        {   
            var sut = new CertificateFinder();
            var result = sut.Find(X509FindType.FindByThumbprint, "somethingthatdoesnotexist");

            Assert.False(result.Found);

        }

        [Fact]
        public void Find_FindsAStoredCertificateByThumbprint()
        {
            var certificate = CreateCertificateIn(StoreName.My, StoreLocation.CurrentUser);
            var sut = new CertificateFinder();
            
            var result = sut.Find(X509FindType.FindByThumbprint, certificate.Thumbprint);

            Assert.True(result.Found);

        }
    }
}