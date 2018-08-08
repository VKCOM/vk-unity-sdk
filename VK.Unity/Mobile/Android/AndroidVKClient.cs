using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VK.Unity.Requests;
using VK.Unity.Responses;
using VK.Unity.Results;
using VK.Unity.Utils;

namespace VK.Unity.Mobile.Android
{
    class AndroidVKClient : VKClientBase
    {
     

        internal AndroidVKClient() 
        {
        }

        public override void Init(VKInitParams initParams, Action initializedCallback) 
        {
            base.Init(initParams, initializedCallback);
            InitRequest initRequest = new InitRequest() { appId = (int)initParams.AppId, apiVersion = initParams.ApiVersion};
            string jsonStr = JsonHelper.ToJson(initRequest);
            AndroidNativeHelper.CallStatic("init", jsonStr);            
        }


        public override void LogIn(IEnumerable<Scope> scopes, Action<AuthResponse> callback = null)
        {
            base.LogIn(scopes, callback);
            LoginRequest loginRequest = new LoginRequest { scopes = scopes.Select(s=>Enum.GetName(typeof(Scope), s)).ToList() };
            string jsonStr = JsonHelper.ToJson(loginRequest);
            AndroidNativeHelper.CallStatic("login", jsonStr);            
        }

        public override void LogOut()
        {
            base.LogOut();
            AndroidNativeHelper.CallStatic("logout");
        }

        public override void OnInitComplete(string resultStr)
        {
            var data = Json.Deserialize(resultStr) as Dictionary<string, object>;          
            
            if (data.ContainsKey("certificateFingerprint"))
            {
                VKSDK._extras.Add(VKSDK.EXTRA_ANDROID_CERTIFICATE_FINGERPRINT_KEY, data["certificateFingerprint"].ToString());
            }

            base.OnInitComplete(resultStr);

        }

        public override void API(string method, IDictionary<string, string> queryParams, Action<APICallResponse> callback = null)
        {        
            String callbackId = "";
            if (callback != null)
            {
                callbackId = callbackManager.RegisterCallback(callback).ToString();
            }

            List<String> paramsList = new List<String>();
            foreach(var kvp in queryParams)
            {
                paramsList.Add(kvp.Key);
                paramsList.Add(kvp.Value);
            }

            APICallRequest apiCallRequest = new APICallRequest() { callbackId = callbackId, methodName = method, parameters = paramsList };
            string jsonStr = JsonHelper.ToJson(apiCallRequest);
            AndroidNativeHelper.CallStatic("apiCall", jsonStr);
        }
     }
}
