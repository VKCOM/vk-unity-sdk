using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VK.Unity.Utils;

namespace VK.Unity.Responses
{
    [Serializable]
    public class ResponseBase
    {
        public VKError error;

        public String callbackId;


        public override string ToString()
        {
            return JsonHelper.ToJson(this);
        }
    }
}
