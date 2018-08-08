using AOT;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using VK.Unity.Requests;
using VK.Unity.Responses;
using VK.Unity.Utils;

namespace VK.Unity.Web
{
    class WebVKClient : VKClientBase
    {
        protected static readonly string MESSAGE_UNSUPPORTED_PLATFORM = "WebVKClient: Unsupported method for this platform";
        protected new static CallbackManager callbackManager;

        internal WebVKClient()
        {
            callbackManager = new CallbackManager();
        }

        public override void LogOut()
        {
            VKLogger.Error(MESSAGE_UNSUPPORTED_PLATFORM);
        }

        public override void Init(VKInitParams initParams, Action initializedCallback)
        {
            base.Init(initParams, initializedCallback);
            JSBridge.Handler("Init", initParams.ApiVersion);
        }

        public override void LogIn(IEnumerable<Scope> scope, Action<AuthResponse> callback = null)
        {
            VKLogger.Error(MESSAGE_UNSUPPORTED_PLATFORM);
        }

        public override void API(string APIMethod, IDictionary<string, string> queryParams, Action<APICallResponse> callback = null)
        {
            string callbackId = "";
            if (callback != null)
            {
                callbackId = callbackManager.RegisterCallback(callback).ToString();
            }

            List<string> paramsList = new List<string>();
            foreach (var queryParam in queryParams)
            {
                paramsList.Add(queryParam.Key);
                paramsList.Add(queryParam.Value);
            }

            APICallRequest apiCallRequest = new APICallRequest() { callbackId = callbackId, methodName = APIMethod, parameters = paramsList };
            string requestData = JsonUtility.ToJson(apiCallRequest);
            VKLogger.Info("WebVKClient.API methodName = " + APIMethod + ", args = " + requestData);
            JSBridge.Handler("API", requestData, ResultHandler);
        }

        public override void AddCallback(string eventName, Action<APICallResponse> callback = null)
        {
            string callbackId = "";
            if (callback != null)
            {
                callbackId = callbackManager.RegisterCallback(callback).ToString();
            }

            GenericRequest request = new GenericRequest();
            request.AddString("callbackId", callbackId);
            request.AddString("eventName", eventName);

            string requestData = request.ToJsonString();
            VKLogger.Info("WebVKClient.AddCallback for " + eventName + ", args = " + requestData);
            JSBridge.Handler("AddCallback", requestData, CallbackHandler);
        }

        public override void RemoveCallback(string eventName, string callbackId)
        {
            if (!int.TryParse(callbackId, out int id) || id < 1)
            {
                throw new ArgumentException("Incorrect callback ID");
            }

            GenericRequest request = new GenericRequest();
            request.AddString("callbackId", callbackId);
            request.AddString("eventName", eventName);

            string requestData = request.ToJsonString();
            VKLogger.Info("WebVKClient.RemoveCallback for " + eventName + ", args = " + requestData);
            JSBridge.Handler("RemoveCallback", requestData);
            callbackManager.RemoveCallback(id);
        }

        public override void OnInitComplete(string resultStr)
        {
            VKLogger.Error(MESSAGE_UNSUPPORTED_PLATFORM);
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        public static void ResultHandler(string stringifiedResponse)
        {
            VKLogger.Info("WebVKClient.ResultHandler result = " + stringifiedResponse);

            APICallResponse acr = new APICallResponse();
            acr = JsonUtility.FromJson<APICallResponse>(stringifiedResponse);
            acr.responseJsonString = WWW.UnEscapeURL(acr.responseJsonString);

            callbackManager.HandleResponse(acr);
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        public static void CallbackHandler(string stringifiedResponse)
        {
            VKLogger.Info("WebVKClient.CallbackHandler result = " + stringifiedResponse);

            APICallResponse acr = new APICallResponse();
            acr = JsonUtility.FromJson<APICallResponse>(stringifiedResponse);
            acr.responseJsonString = WWW.UnEscapeURL(acr.responseJsonString);

            callbackManager.HandleCallback(acr);
        }

        private class JSBridge : MonoBehaviour
        {
            [DllImport("__Internal")]
            public static extern void Handler(string method, string param, Action<string> callbackAction);

            [DllImport("__Internal")]
            public static extern void Handler(string method, string param);
        }
    }
}