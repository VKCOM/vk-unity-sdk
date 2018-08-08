using System.Collections.Generic;

namespace VK.Unity.Utils
{
    internal static class Utilities
    {
        public static string ToJson(this IDictionary<string, object> dictionary)
        {
            return Json.Serialize(dictionary);
        }

        public static bool TryGetValue<T>(
            this IDictionary<string, object> dictionary,
            string key,
            out T value)
        {
            object resultObj;
            if (dictionary.TryGetValue(key, out resultObj) && resultObj is T)
            {
                value = (T)resultObj;
                return true;
            }

            value = default(T);
            return false;
        }
    }
}
