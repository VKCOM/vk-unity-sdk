using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VK.Unity.Requests
{
    [Serializable]
    public class InitRequest : RequestBase
    {
        public int appId;

        public string apiVersion;
    }
}
