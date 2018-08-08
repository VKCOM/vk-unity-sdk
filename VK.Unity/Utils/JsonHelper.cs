using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace VK.Unity.Utils
{
    public class JsonHelper
    {
        public static string ToJson(object obj)
        {
            return JsonUtility.ToJson(obj);
        }

        public static T FromJson<T>(string jsonStr)
        {
            return JsonUtility.FromJson<T>(jsonStr);
        }

    }
}
