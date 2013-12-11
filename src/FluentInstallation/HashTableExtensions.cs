using System.Collections;
using System.Linq;

namespace FluentInstallation
{
    public static class HashTableExtensions
    {
        public static object GetValueWithLowerInvariantKey(this Hashtable hashtable, string keyname)
        {
            var key = hashtable.Keys.Cast<string>().FirstOrDefault(k => k.ToLowerInvariant() == keyname.ToLowerInvariant());

            if (key == null)
                return null;

            return hashtable[key];
        }
    }
}