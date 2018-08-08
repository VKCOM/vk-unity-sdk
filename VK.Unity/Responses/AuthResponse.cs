using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VK.Unity.Responses
{
    [Serializable]
    public class AuthResponse : ResponseBase
    {
        public string accessToken;

        public int expiresIn;

        public int userId;     
    }
}
