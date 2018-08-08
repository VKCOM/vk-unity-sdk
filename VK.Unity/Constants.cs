using UnityEngine;

namespace VK.Unity
{
    internal static class Constants
    {
        public const string CALLBACK_ID_KEY = "callback_id";
        public const string ACCESS_TOKEN_KEY = "access_token";
        public const string RESPONSE_KEY = "response";

        private static VKUnityPlatform? _currentPlatform;

        public static bool IsMobile
        {
            get
            {
                VKUnityPlatform currentPlatform = CurrentPlatform;
                return currentPlatform == VKUnityPlatform.Android || currentPlatform == VKUnityPlatform.iOS;
            }
        }

        public static bool IsEditor => Application.isEditor;

        public static bool IsWeb => CurrentPlatform == VKUnityPlatform.WebGL;

        public static bool IsDesktop => CurrentPlatform == VKUnityPlatform.Windows;

        public static bool IsDebugBuild => Debug.isDebugBuild;

        public static VKUnityPlatform CurrentPlatform
        {
            get
            {
                if (!_currentPlatform.HasValue)
                {
                    _currentPlatform = GetCurrentPlatform();
                }

                return _currentPlatform.Value;
            }
            set
            {
                _currentPlatform = value;
            }
        }

        private static VKUnityPlatform GetCurrentPlatform()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    return VKUnityPlatform.Android;
                case RuntimePlatform.IPhonePlayer:
                    return VKUnityPlatform.iOS;
                case RuntimePlatform.WebGLPlayer:
                    return VKUnityPlatform.WebGL;
                case RuntimePlatform.WindowsPlayer:
                    return VKUnityPlatform.Windows;
                default:
                    return VKUnityPlatform.Unknown;
            }
        }
    }
}
