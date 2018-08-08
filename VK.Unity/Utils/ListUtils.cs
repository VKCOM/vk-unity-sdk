using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VK.Unity.Utils
{
    static class ListUtils
    {
        public static string GetCommaSeparated(this List<object> ids, string separator = ",")
        {
            StringBuilder sb = new StringBuilder();

            int count = ids.Count;

            for (int i = 0; i < count; i++)
            {
                sb = sb.Append(ids[i].ToString());
                if (i != count - 1)
                {
                    sb = sb.Append(separator);
                }
            }

            return sb.ToString();
        }
    }
}
