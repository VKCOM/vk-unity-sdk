using System.Collections.Generic;
using UnityEngine;

namespace VK.Unity
{
    /// <summary>
    /// VK settings.
    /// </summary>
    public class VKSettings : ScriptableObject
    {
        public const string ASSET_NAME = "VKSettings";
        public const string ASSET_EXTENSION = ".asset";
        public const string PATH = "VKSDK/SDK/Resources";

        public delegate void OnChangeCallback();
        private static readonly List<OnChangeCallback> _onChangeCallbacks = new List<OnChangeCallback>();
        private static VKSettings _instance;

        public static readonly string DefaultAPIVersion = "5.62";

        [SerializeField]
        private long _appId;

        [SerializeField]
        private string _apiVersion = DefaultAPIVersion;

        public static VKSettings Instance
        {
            get
            {
                _instance = NullableInstance ?? CreateInstance<VKSettings>();
                return _instance;
            }
        }

        public static VKSettings NullableInstance => _instance ?? (_instance = Resources.Load(ASSET_NAME) as VKSettings);

        public static long AppId
        {
            get { return Instance._appId; }
            set
            {
                if (Instance._appId != value)
                {
                    Instance._appId = value;
                    OnSettingsChanged();
                }
            }
        }

        public static string ApiVersion
        {
            get { return Instance._apiVersion; }
            set
            {
                if (Instance._apiVersion != value)
                {
                    Instance._apiVersion = value;
                    OnSettingsChanged();
                }
            }
        }

        public static void RegisterChangeCallback(OnChangeCallback callback)
        {
            _onChangeCallbacks.Add(callback);
        }

        private static void OnSettingsChanged()
        {
            foreach (OnChangeCallback callback in _onChangeCallbacks)
            {
                callback?.Invoke();
            }
        }
    }
}
