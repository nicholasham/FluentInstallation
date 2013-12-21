using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Security.Cryptography.X509Certificates;

namespace FluentInstallation
{
    public interface IInstallerContext
    {
        IDictionary Parameters { get; }
      
        ILogger Logger { get; }
    }

    public interface ILogger
    {
        void WriteDebug(string message, params object[] args);
        void WriteVerbose(string message, params object[] args);
        void WriteWarning(string message, params object[] args);
        void WriteError(Exception exception);
    }

    public class CommandLogger : ILogger
    {
        private readonly ICommand _command;

        public CommandLogger(ICommand command)
        {
            _command = command;
        }

        public void WriteDebug(string message, params object[] args)
        {
            _command.WriteDebug(string.Format(message, args));
        }

        public void WriteVerbose(string message, params object[] args)
        {
            _command.WriteVerbose(string.Format(message, args));
        }

        public void WriteWarning(string message, params object[] args)
        {
            _command.WriteWarning(string.Format(message, args));
        }

        public void WriteError(Exception exception)
        {
            _command.WriteError(new ErrorRecord(exception, string.Empty, ErrorCategory.NotSpecified, _command));
        }
    }
}