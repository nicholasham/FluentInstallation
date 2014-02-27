using System;
using Microsoft.Web.Administration;

namespace FluentInstallation.Web
{
    public static class WebExceptions
    {
        public static Exception NoSiteFoundMatchingName(string name)
        {
            return new InstallationException(Message("Unable to find any site on the server with a name matching {0}. ", name));
        }

        public static Exception NoApplicationPoolFoundMatchingName(string name)
        {
            return new InstallationException(Message("Unable to find any application pool on the server with a name matching {0}. ", name));
        }

        public static Exception NoCertificateFoundMatchingThumbprint(string thumbprint)
        {
            return new InstallationException(Message("Unable to find a certificate on machine {0} with a certificate matching thumbprint {1}", Environment.MachineName, thumbprint));
        }

        static string Message(string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static Exception ApplicationNotFoundInSite(Site site, string @alias)
        {
            return new InstallationException(Message("Unable to find application in site {0} with alias {1}.", site.Name, alias));
        }


        public static Exception VirtualDirectoryNotFoundInSite(Site site, string @alias)
        {
            return new InstallationException(Message("Unable to find virtual directory in site {0} with alias {1}.", site.Name, alias));
        }
    }

}