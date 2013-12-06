using System;
using FluentInstallation.Builders;

namespace FluentInstallation.TestAssembly
{
    public class ProjectInstaller : IInstaller
    {
        public void Install(IInstallerContext context)
        {
            context
                .ConfigureIIS()
                    .DeleteApplicationPool("mysite.com")
                    .DeleteApplication("assets")
                        .ContainedInWebsite("mysite.com")
                    .DeleteWebsite("mysite.com")
                    .Commit();

            context
                .ConfigureIIS()
                    .CreateApplicationPool(applicationPool =>
                    {
                        applicationPool.Named("mysite.com");
                        applicationPool.UsingClassicPipelineMode();
                        applicationPool.UsingCustomIdentity(identity =>
                        {
                            identity.WithUsername("nick");
                            identity.WithPassword("password");
                        });

                    })
                    .Commit();

            context
                .ConfigureIIS()
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