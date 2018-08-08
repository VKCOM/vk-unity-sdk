
using VK.Unity.Mobile.Android;
using VK.Unity.Web;

namespace VK.Unity.Utils
{
    internal static class VKLoaderHelper
    {
        /// <summary>
        /// Adds and returns a loader component depending on the current platform
        /// </summary>
        /// <returns></returns>
        public static VKSDK.VKLoader GetLoaderForCurrentPlatform()
        {
            

            //if (Constants.IsEditor)
            //{
            //    return ComponentFactory.GetComponent<EditorLoader>();
            //}

            switch (Constants.CurrentPlatform)
            {
                case VKUnityPlatform.Android: return ComponentFactory.GetComponent<AndroidVKLoader>();
                case VKUnityPlatform.WebGL: return ComponentFactory.GetComponent<WebVKLoader>();
            }

            return null;
        }
    }
}
