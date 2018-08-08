using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VK.Unity.Requests
{
    [Serializable]
    public class APICallRequest : RequestBase
    {
        public string methodName;

        public List<String> parameters;
    }
}
