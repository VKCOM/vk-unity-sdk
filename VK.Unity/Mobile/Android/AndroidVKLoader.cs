using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VK.Unity.Mobile.Android
{
    class AndroidVKLoader : VKSDK.VKLoader
    {
        protected override VKGameManager CreateManager()
        {
            var androidVK = ComponentFactory.GetComponent<AndroidVKGameManager>();
            if (androidVK.Client == null)
            {
                androidVK.Client = new AndroidVKClient();
            }

            return androidVK;
        }
    }
}
