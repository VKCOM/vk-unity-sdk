using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VK.Unity.Utils;

namespace VK.Unity.Requests
{
    class GenericRequest
    {
        private Dictionary<string, object> arguments = new Dictionary<string, object>();

        public void AddString(string argName, string val)
        {
            if (!string.IsNullOrEmpty(val))
            {
                arguments[argName] = val;
            }
        }

        public string ToJsonString()
        {
            return Json.Serialize(arguments);
        }

    }
}
