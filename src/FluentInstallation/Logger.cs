using System;

namespace FluentInstallation
{
    public abstract class Logger : ILogger
    {
        public abstract void Debug(string message, params object[] args);
       
        public void Debug(Action<IMessageBuilder> message)
        {
            Debug(BuildMessage(message));
        }

        private static string BuildMessage(Action<IMessageBuilder> message)
        {
            var builder = new MessageBuilder();
            message(builder);
            return builder.Build();
        }

        public abstract void Info(string message, params object[] args);

        public void Info(Action<IMessageBuilder> message)
        {
            Info(BuildMessage(message));
        }

        public abstract void Warn(string message, params object[] args);

        public void Warn(Action<IMessageBuilder> message)
        {
            Warn(BuildMessage(message));
        }

        public abstract void Error(string message, params object[] args);

        public void Error(Action<IMessageBuilder> message)
        {
            Error(BuildMessage(message));
        }
    }
}