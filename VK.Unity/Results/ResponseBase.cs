using System.Collections.Generic;
using VK.Unity.Utils;

namespace VK.Unity.Results
{
    public class ResponseBase : ICallbackReponse
    {
        internal ResponseBase(VKResponseContainer container)
        {
            int callbackId = GetCallbackId(container.ResultDictionary);

            Init(container, callbackId);
        }

        private static int GetCallbackId(IDictionary<string, object> result)
        {
            if (result == null)
            {
                return -1;
            }

            // Check for cancel string
            int callbackId;
            if (result.TryGetValue(Constants.CALLBACK_ID_KEY, out callbackId))
            {
                return callbackId;
            }

            return -1;
        }

        private void Init(VKResponseContainer container, int callbackId)
        {
            JsonString = container.JsonString;
            ResultDictionary = container.ResultDictionary;
            CallbackId = callbackId;
        }

        public IDictionary<string, object> ResultDictionary { get; protected set; }
        public string JsonString { get; protected set; }
        public string Error { get; protected set; }
        public int CallbackId { get; protected set; }
    }
}
