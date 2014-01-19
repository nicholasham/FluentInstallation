using System;

namespace FluentInstallation
{
    public interface ILogger
    {
        void Debug(string message, params object[] args);
        void Debug(Action<IMessageBuilder> message);
        void Info(string message, params object[] args);
        void Info(Action<IMessageBuilder> message);
        void Warning(string message, params object[] args);
        void Warning(Action<IMessageBuilder> message);
        void Error(string message, params object[] args);
        void Error(Action<IMessageBuilder> message);
    }
}