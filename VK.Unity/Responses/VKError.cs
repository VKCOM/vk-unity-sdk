using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VK.Unity.Responses
{
    [Serializable]
    public class VKError
    {
        public int errorCode;

        public string errorMessage;

        public string errorReason;
    }
}
