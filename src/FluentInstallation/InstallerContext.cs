using System;
using System.Linq.Expressions;
using System.Management.Automation;
using System.Linq;

namespace FluentInstallation
{
    public class InstallerContext : IInstallerContext
    {
        private ICommand Command { get; set; }
        
        public InstallerContext(ICommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            if (command.Parameters == null)
            {
                throw new ArgumentException("The command parameters can not be null", "command");
            }
            
            
            Command = command;
        }

        public T GetParameters<T>() where T : class, new()
        {
            var result = new T();
            

            foreach (var property in typeof (T).GetProperties())
            {
                var required = property.GetCustomAttributes(false).Any(a => a.GetType() == typeof (RequiredAttribute));

                object keyValue = Command.Parameters.GetValueWithLowerInvariantKey(property.Name);

                if (required && keyValue == null)
                    throw new RequiredParameterNotGivenException(property.Name);

                if (keyValue != null)
                {
                    if (property.PropertyType != typeof(string))
                    {
                        try
                        {
                            keyValue = Convert.ChangeType(keyValue, property.PropertyType);
                        }
                        catch (FormatException)
                        {
                            throw new ParameterCastException(property.Name, property.PropertyType);
                        }
                    }
                    property.SetValue(result, keyValue, null);
                }


            }



            return result;
        }

        public void WriteDebug(string message)
        {
            Command.WriteDebug(message);
        }

        public void WriteVerbose(string message)
        {
            Command.WriteVerbose(message);
        }

        public void WriteWarning(string message)
        {
            Command.WriteWarning(message);
        }

        public void WriteCommandDetail(string message)
        {
            Command.WriteCommandDetail(message);
        }
    }
}