using System;

namespace FluentInstallation
{
    public static class Catch
    {
        public static Exception Exception(Action throws)
        {
            Exception result = null;

            try
            {
                throws();
            }
            catch (Exception exception)
            {
                result = exception;
            }

            return result;
        }
    }
}