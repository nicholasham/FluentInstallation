namespace FluentInstallation
{
    public class RequiredParameterNotGivenException : InstallationException
    {
        public RequiredParameterNotGivenException(string parameterName)
            : base(string.Format("Parameter {0} is required please specify it.", parameterName))
        {

        }
    }
}