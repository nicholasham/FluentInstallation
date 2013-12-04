namespace FluentInstallation
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInstaller
    {
        /// <summary>
        /// Performs the installation
        /// </summary>
        void Install(IInstallerContext context);

        /// <summary>
        /// Removes the installation
        /// </summary>
        void Uninstall(IInstallerContext context);

    }
}