using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using VK.Unity.Utils;

namespace VK.Unity.Mobile.Android
{
    class AndroidNativeHelper
    {        
        private static AndroidJavaClass vkJavaClass = new AndroidJavaClass("com.vk.unity.VK");

        public static void CallStatic(string methodName, params object[] args)
        {            
            VKLogger.Info("AndroidNativeHelper.CallStatic methodName = " + methodName + ", args = " + args.ToList().GetCommaSeparated());
            vkJavaClass.CallStatic(methodName, args);
        }

    }
}
