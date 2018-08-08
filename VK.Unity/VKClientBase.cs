using System;
using System.Collections.Generic;
using VK.Unity.Responses;
using VK.Unity.Results;
using VK.Unity.Utils;

namespace VK.Unity
{
    abstract class VKClientBase : IVKClientWithResultHandler
    {
        private Action _initializedCallback;
        private Action<AuthResponse> _loggedInCallback;

        protected CallbackManager callbackManager;

        internal VKClientBase()
        {
            callbackManager = new CallbackManager();
        }


        public virtual void LogOut()
        {
            AccessToken.Current = null;
        }

        //public void API(string method, IDictionary<string, string> queryParams, Action<string> callback = null)
        //{
        //    var inputQueryParams = queryParams != null ? CopyByValue(queryParams) : new Dictionary<string, string>();

        //    inputQueryParams.AddApiVersion();
        //    inputQueryParams.AddAccessToken();

        //    string url = VKUrlBuilder.BuildRequestUrl(method);

        //    AsyncRequest.Begin(url, inputQueryParams, callback);
        //}

        public virtual void Init(VKInitParams initParams, Action initializedCallback)
        {
            if (_initializedCallback != null)
            {
                throw new InvalidOperationException("Init operation is called twice.");
            }

            _initializedCallback = initializedCallback;
        }

        public virtual void LogIn(IEnumerable<Scope> scope, Action<AuthResponse> callback = null)
        {
            if (_loggedInCallback != null)
            {
                throw new InvalidOperationException("Login operation is called twice.");
            }

            _loggedInCallback = callback;
        }


        public abstract void API(string method, IDictionary<string, string> queryParams, Action<APICallResponse> callback = null);

        public virtual void AddCallback(string eventName, Action<APICallResponse> callback) { }

        public virtual void RemoveCallback(string eventName, string callbackId) { }

        public virtual void OnInitComplete(string resultStr)
        {
            VKLogger.Info("VKClientBase.OnInitComplete: resultStr = " + resultStr);
            HandleAuthResponse(JsonHelper.FromJson<AuthResponse>(resultStr));

            if (_initializedCallback != null)
            {
                _initializedCallback();
                _initializedCallback = null;
            }
        }

        public  void OnLoginComplete(string  resultStr)
        {
            VKLogger.Info("VKClientBase.OnLoginComplete: resultStr = " + resultStr);
            var authResponse = JsonHelper.FromJson<AuthResponse>(resultStr);

            HandleAuthResponse(authResponse);

            if (_loggedInCallback != null)
            {
                _loggedInCallback(authResponse);
                _loggedInCallback = null;
            }
        }
 

        public void OnAPICallComplete(string responseStr)
        {
            VKLogger.Info("VKClientBase.OnAPICallComplete: resultStr = " + responseStr);
            var apiCallResponse = JsonHelper.FromJson<APICallResponse>(responseStr);
            callbackManager.HandleResponse(apiCallResponse);
        }
   


        protected void HandleAuthResponse(AuthResponse authResponse)
        {
            if (!string.IsNullOrEmpty(authResponse.accessToken)) {
                AccessToken.Current = new AccessToken(authResponse.accessToken, authResponse.expiresIn, authResponse.userId);
            }
        }

        public void OnAccessTokenChanged(string messageStr)
        {
            var authResponse = JsonHelper.FromJson<AuthResponse>(messageStr);
            if (!string.IsNullOrEmpty(authResponse.accessToken))
            {
                AccessToken.Current = new AccessToken(authResponse.accessToken, authResponse.expiresIn, authResponse.userId);
            }
            else
            {
                AccessToken.Current = null;
            }

            VKSDK.NotifyAccessTokenChanged();
        }
    }
}
