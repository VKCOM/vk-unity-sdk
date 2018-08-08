using System;
using System.Collections.Generic;
using VK.Unity.Responses;
using VK.Unity.Results;

namespace VK.Unity
{
    internal interface IVKClient
    {
        void Init(VKInitParams initParams, Action initializedCallback);

        void LogIn(
            IEnumerable<Scope> scope,
            Action<AuthResponse> callback = null);

        void LogOut();

        void API(
            string method,
            IDictionary<string, string> queryParams,
            Action<APICallResponse> callback = null);

        void AddCallback(string eventName, Action<APICallResponse> callback = null);

        void RemoveCallback(string eventName, string callbackId);
    }
}
