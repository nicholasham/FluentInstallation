using System;
using System.Collections;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using NSubstitute;
using Xunit;

namespace FluentInstallation.IntegrationTests
{

    
    public class TestInstallerContext : InstallerContext
    {
        public TestInstallerContext(Hashtable hashtable) : base(CreateCommand(hashtable))
        {

        }

        private static ICommand CreateCommand(Hashtable hashtable)
        {
            var command = Substitute.For<ICommand>();
            command.Parameters.Returns(hashtable);
            return command;
        }
    }

    
    public class InstallerTests
    {

        public IInstallerContext CreateInstallerContext(Hashtable parameters)
        {
            return new TestInstallerContext(parameters);
        }



        
        [Fact]
        public void CanInstallWebsiteCorrectly()
        {

            var context = CreateInstallerContext(new Hashtable());
            
            context
                .ConfigureWebServer()
                .DeleteWebsite("Test")
                .CreateWebsite(website =>
                {
                    website.Named("Test");
                    website.OnPhysicalPath(@"C:\");

                    website.AddBinding(binding =>
                    {
                        binding.OnPort(9090);
                        binding.UseProtocol("http");
                    });
                })
                .Commit();

         }

        
    }




    
}