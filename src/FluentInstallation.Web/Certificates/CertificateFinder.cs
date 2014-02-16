using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace FluentInstallation.Web.Certificates
{
    /// <summary>
    ///     Does a search on the local computer looking for the first certificate matching the search criteria
    /// </summary>
    public class CertificateFinder : ICertificateFinder
    {
        public CertificateFindResult Find(X509FindType findType, object findValue)
        {
            string[] storeLocations = Enum.GetNames(typeof (StoreLocation));
            string[] storeNames = Enum.GetNames(typeof (StoreName));

            foreach (string storeLocation in storeLocations)
            {
                var location = (StoreLocation) Enum.Parse(typeof (StoreLocation), storeLocation);

                foreach (string storeName in storeNames)
                {
                    var name = (StoreName) Enum.Parse(typeof (StoreName), storeName);

                    var store = new X509Store(name, location);

                    store.Open(OpenFlags.ReadOnly);

                    try
                    {
                        IEnumerable<X509Certificate2> result =
                            store.Certificates.Find(findType, findValue, false)
                                 .Cast<X509Certificate2>().ToArray();

                        if (result.Any())
                        {
                            return new CertificateFindResult(location, name, result.First());
                        }
                    }
                    finally
                    {
                        store.Close();
                    }
                }
            }

            return new CertificateFindResult();
        }
    }
}