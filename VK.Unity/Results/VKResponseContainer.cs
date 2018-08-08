using System.Collections.Generic;
using VK.Unity.Utils;

namespace VK.Unity.Results
{
    public class VKResponseContainer
    {
        public VKResponseContainer(IDictionary<string, object> dictionary)
        {
            JsonString = dictionary.ToJson();
            ResultDictionary = dictionary;
        }

        public VKResponseContainer(string jsonString)
        {
            JsonString = jsonString;

            if (string.IsNullOrEmpty(jsonString))
            {
                ResultDictionary = new Dictionary<string, object>();
            }
            else
            {
                ResultDictionary = Json.Deserialize(jsonString) as Dictionary<string, object>;
            }
        }

        public string JsonString { get; private set; }

        public IDictionary<string, object> ResultDictionary { get; set; }
    }
}
