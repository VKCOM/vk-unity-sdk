using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VK.Unity.Requests
{
    [Serializable]
    public class LoginRequest : RequestBase
    {
        public List<string> scopes;
    }
}
