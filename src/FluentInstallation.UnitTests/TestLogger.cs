using System;

namespace FluentInstallation
{
    public class TestLogger : Logger
    {
        public override void Debug(string message, params object[] args)
        {
            LogToConsole(message);
        }

        private static void LogToConsole(string message)
        {
            Console.WriteLine("{0:yyyy-MM-ddTHH:mm:ss.fffffff00K} {1}", DateTime.Today, message);
        }

        public override void Info(string message, params object[] args)
        {
            LogToConsole(message);
        }

        public override void Warn(string message, params object[] args)
        {
            LogToConsole(message);

        }

        public override void Error(string message, params object[] args)
        {
            LogToConsole(message);

        }

        public override void Error(Exception exception)
        {
            LogToConsole(exception.Message);
        }
    }
}