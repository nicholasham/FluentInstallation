using System;
using System.Security.Cryptography.X509Certificates;
using FluentInstallation.WebAdministration;
using Microsoft.Web.Administration;

namespace FluentInstallation.TestAssembly
{

    public class ProjectParameters
    {
        public string SiteName { get; set; }
    }

    [DefaultInstaller]
    public class ProjectInstaller : IInstaller
    {
        public void Install(IInstallerContext context)
        {

            var parameters = context.Parameters.Cast<ProjectParameters>();

            context
                .ConfigureWebServer()
                    .DeleteApplicationPool(parameters.SiteName)
                    .DeleteWebsite(parameters.SiteName)
                    .Commit();   


            context
                .ConfigureWebServer()
                    .CreateApplicationPool(applicationPool =>
                    {
                        applicationPool.Named("TestAppPool");
                        applicationPool.UseClassicPipelineMode();
                        applicationPool.UseCustomIdentity("Nick", "password");
                    })
                    .Commit();


            context
                .ConfigureWebServer()
                    .CreateWebsite(site =>
                    {
                        site.Named(parameters.SiteName);
                        site.OnPhysicalPath(@"C:\");
                        site.UseApplicationPool(parameters.SiteName);

                        site.AddBinding(binding =>
                        {
                            binding.UseProtocol("https");
                            binding.OnPort(80);
                            binding.UseHostName("local.site.com");
                            binding.UseCertificateWithThumbprint("8e6e3cc19bf5abfe01c7ee12ea23f20f4a1d513c");
                        });

                        site.AddApplication(application =>
                        {
                            application.UseAlias("funkyapi");
                            application.OnPhysicalPath(@".\api");
                        });

                        site.AddVirtualDirectory(virtualDirectory =>
                        {
                            virtualDirectory.UseAlias("assets");
                            virtualDirectory.OnPhysicalPath(@".\Assets");
                        });
                    })
                    .Commit();
            
        }

        public void Uninstall(IInstallerContext context)
        {
            throw new NotImplementedException();
        }
    }
}