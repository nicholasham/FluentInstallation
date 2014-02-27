using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace FluentInstallation
{
    public static class ParametersCastExtensions
    {
        public static object GetValueWithLowerInvariantKey(this IDictionary dictionary, string keyname)
        {
            string key =
                dictionary.Keys.Cast<string>().FirstOrDefault(k => k.ToLowerInvariant().Replace(".", "") == keyname.ToLowerInvariant());

            if (key == null)
                return null;

            return dictionary[key];
        }

        public static T Cast<T>(this IDictionary parameters) where T : class, new()
        {
            var result = new T();

            var writableProperties = typeof (T).GetProperties().Where(property => property.CanWrite);

            foreach (PropertyInfo property in writableProperties)
            {
                bool required = property.GetCustomAttributes(false).Any(a => a.GetType() == typeof (RequiredAttribute));

                object keyValue = parameters.GetValueWithLowerInvariantKey(property.Name);

                if (required && keyValue == null)
                    throw new RequiredParameterNotGivenException(property.Name);

                if (keyValue != null)
                {

                    if (property.PropertyType != typeof (string))
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
    }
}