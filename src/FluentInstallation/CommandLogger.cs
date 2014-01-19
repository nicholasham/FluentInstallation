using System;
using System.Management.Automation;

namespace FluentInstallation
{
    public class CommandLogger : Logger
    {
        private readonly ICommand _command;

        public CommandLogger(ICommand command)
        {
            _command = command;
        }

        public override void Debug(string message, params object[] args)
        {
            _command.WriteDebug(string.Format(message, args));
        }

        public override void Info(string message, params object[] args)
        {
            _command.WriteVerbose(string.Format(message, args));
        }

        public override void Warning(string message, params object[] args)
        {
            _command.WriteWarning(string.Format(message, args));
        }

        public override void Error(string message, params object[] args)
        {
            _command.WriteError(new ErrorRecord(new InstallationException(string.Format(message, args)), string.Empty, ErrorCategory.NotSpecified, _command));
        }
    }
}