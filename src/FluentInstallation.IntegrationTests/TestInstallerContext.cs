using System.Collections;
using NSubstitute;

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
}