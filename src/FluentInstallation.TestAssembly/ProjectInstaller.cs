namespace FluentInstallation.TestAssembly
{
    public class ProjectInstaller : IInstaller
    {
        public void Install(IInstallerContext context)
        {

            context.WriteDebug("Debug");
            context.WriteVerbose("Verbose");
            context.WriteWarning("Warning");


            for (int i = 0; i < 1000; i++)
            {
                context.Progress("Hello i am installing stuff " + i);    
            }
            
        }

        public void Uninstall(IInstallerContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}