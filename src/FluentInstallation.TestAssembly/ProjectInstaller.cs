using System;
using FluentInstallation.IIS;

namespace FluentInstallation.TestAssembly
{
    
    
    public class ProjectInstaller : IInstaller
    {
        public void Install(IInstallerContext context)
        {
            context
                .ConfigureWebServer()
                    .DeleteApplicationPool("mysite.com")
                    .DeleteApplication("assets")
                        .ContainedInWebsite("mysite.com")
                    .DeleteWebsite("mysite.com")
                    .Commit();

            context
                .ConfigureWebServer()
                    .CreateApplicationPool(applicationPool =>
                    {
                        applicationPool.Named("mysite.com");
                        applicationPool.UsingClassicPipelineMode();
                        applicationPool.UsingCustomIdentity("Nick", "password");
                    })
                    .Commit();

            context
                .ConfigureWebServer()
                    .CreateWebsite(site =>
                    {
                        site.Named("mysite.com");
                        site.UsingThisAssemblyDirectoryAsPath();
                        site.UsingApplicationPool("mysite.com");

                        site.AddBinding(binding =>
                        {
                            binding.UsingProtocol("https");
                            binding.OnPort(80);
                            binding.UsingHostName("local.site.com");
                            binding.UseSslCertificate("ddsddssds");
                        });

                        site.AddApplication(application =>
                        {
                            application.UsingAlias("funkyapi");
                            application.OnPath(@".\api");
                        });

                        site.AddVirtualDirectory(virtualDirectory =>
                        {
                            virtualDirectory.UsingAlias("assets");
                            virtualDirectory.OnPath(@".\Assets");
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