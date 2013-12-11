using System;

namespace FluentInstallation
{
    public class ParameterCastException : InstallationException
    {
        public ParameterCastException(string parameterName, Type parameterType)
            : base(string.Format("Cannot cast to type {0} for parameter name {1}", parameterType, parameterName))
        {
            
        }
    }
}