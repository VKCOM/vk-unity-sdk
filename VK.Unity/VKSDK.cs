using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using VK.Unity.Responses;
using VK.Unity.Results;
using VK.Unity.Utils;

namespace VK.Unity
{
    public class VKSDK : ScriptableObject
    {
        private static readonly string VK_QUERY_STRING_APP_ID = "api_id";
        public static readonly string EXTRA_ANDROID_CERTIFICATE_FINGERPRINT_KEY = "certFingerprint";

        private static IVKClientWithResultHandler _client;

        internal static Dictionary<string, string> _extras = new Dictionary<string, string>();


        public static string SDKVersion => "0.9";


        /// <summary>
        /// Gets the app id.
        /// </summary>
        public static long AppId { get; private set; }

        internal static IVKClientWithResultHandler Client
        {
            get
            {                
                return _client;
            }
            set
            {
                _client = value;
            }
        }

        private static bool _isInitialized;


        public static Action<AccessToken> OnAccessTokenChanged
        {
            get;set;
        }

        public static bool IsInitialized
        {
            get { return _isInitialized; }
        }

        public static bool IsLoggedIn
        {
            get { return AccessToken.Current != null; }
        }

        public static AccessToken AccessToken
        {
            get { return AccessToken.Current; }
        }

        public static int UserId
        {
            get { return AccessToken != null ? AccessToken.UserId : 0; }
        }

        public static string GetExtraData(string key)
        {
            if (_extras.ContainsKey(key))
            {
                return _extras[key];
            }
            return "";
        }

        /// <summary>
        /// Initialize VK SDK with params specified on VK -> Edit Settings page
        /// </summary>
        /// <param name="initializedCallback"></param>
        public static void Init(Action initializedCallback = null)
        {
            VKInitParams initParams = new VKInitParams();

            initParams.AppId = VKSettings.AppId;
            initParams.ApiVersion = VKSettings.ApiVersion;

            Init(initParams, initializedCallback);
        }
        
        private static long ParseApplicationIdFromQueryString()
        {
            int queryStartPos = Application.absoluteURL.IndexOf("?");
            if (queryStartPos == -1)
            {
                throw new Exception("Application must be running as VK IFrame application");
            }

            string query = Application.absoluteURL.Substring(queryStartPos + 1);
            foreach (string queryItem in Regex.Split(query, "&"))
            {
                string[] keyValue = Regex.Split(queryItem, "=");
                if (keyValue[0] == VK_QUERY_STRING_APP_ID)
                {
                    return long.Parse(keyValue[1]);
                }
            }
            return -1;
        }

        public static void Init(VKInitParams initParams, Action initializedCallback = null)
        {
            if (initParams == null)
            {
                throw new ArgumentNullException(nameof(initParams));
            }

            VKLogger.Info("VKSDK.Init: appId = " + initParams.AppId + ", apiVersion = " + initParams.ApiVersion ?? "[none]");

            long appId = initParams.AppId;
            if (Application.platform == RuntimePlatform.WebGLPlayer && appId != ParseApplicationIdFromQueryString())
            {
                throw new ArgumentException("AppId (" + appId + ") doesn't match appId (" + ParseApplicationIdFromQueryString() + ") from the query string.");    
            }

            if (appId <= 0)
            {
                throw new ArgumentException("Incorrect appId");
            }

            AppId = appId;

            if (!_isInitialized)
            {
                _isInitialized = true;

                VKLoader loader = VKLoaderHelper.GetLoaderForCurrentPlatform();

                if (loader == null)
                {
                    VKLogger.Warn("VK does not yet support this platform.");
                }
                else
                {
                    loader.InitParams = initParams;
                    loader.InitializedCallback = initializedCallback;
                }
            }
            else
            {
                VKLogger.Warn("VK.Init() has already been called. You only need to call this once and only once.");
            }
        }

        public static void Login(IEnumerable<Scope> scope, Action<AuthResponse> callback = null)
        {
            Client.LogIn(scope, callback);
        }

        public static void Logout()
        {
            Client.LogOut();
        }
        
        public static void AddCallback(string eventName, Action<APICallResponse> callback = null)
        {
            Client.AddCallback(eventName, callback);
        }

        public static void RemoveCallback(string eventName, string callbackId)
        {
            Client.RemoveCallback(eventName, callbackId);
        }

        /// <summary>
        /// Makes a call to the VK API.
        /// </summary>
        public static void API(
            string method,
            IDictionary<string, string> queryParams, 
            Action<APICallResponse> callback = null)
        {
            Client.API(method, queryParams, callback);
        }

        internal static void NotifyAccessTokenChanged()
        {            
            if (OnAccessTokenChanged != null)
            {
                OnAccessTokenChanged(AccessToken.Current);
            }   
        }

        internal abstract class VKLoader : MonoBehaviour
        {
            protected abstract VKGameManager CreateManager();

            public VKInitParams InitParams { get; set; }
            public Action InitializedCallback { get; set; }

            public void Start()
            {
                VKGameManager manager = CreateManager();

                _client = manager.Client;
                OnDLLLoaded();

                Destroy(this);
            }

            private void OnDLLLoaded()
            {
                _client.Init(InitParams, InitializedCallback);
            }
        }
    }
}
