using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VK.Unity.Utils
{
    internal class AsyncRequest : MonoBehaviour
    {
        private string _url;
        private IDictionary<string, string> _queryParams;
        private Action<string> _callback;

        internal static void Begin(
            string url,
            IDictionary<string, string> queryParams,
            Action<string> callback = null)
        {
            ComponentFactory.AddComponent<AsyncRequest>()
                .SetUrl(url)
                .SetQueryParams(queryParams)
                .SetCallback(callback);
        }

        // returning an IEnumerator makes the request perform asynchronously
        internal IEnumerator Start()
        {
            WWW www;

            if (_queryParams != null && _queryParams.Count > 0)
            {
                string queryParams = _url.Contains("?") ? "&" : "?";

                int i = 0;
                foreach (string key in _queryParams.Keys)
                {
                    string value = _queryParams[key];

                    queryParams += $"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(value)}";

                    if (i < _queryParams.Count)
                    {
                        queryParams += "&";
                    }

                    i++;
                }

                www = new WWW(_url + queryParams);
            }
            else
            {
                www = new WWW(_url);
            }

            yield return www;

            _callback?.Invoke(www.text);

            www.Dispose();

            Destroy(this);
        }

        private AsyncRequest SetUrl(string url)
        {
            _url = url;
            return this;
        }

        private AsyncRequest SetQueryParams(IDictionary<string, string> queryParams)
        {
            _queryParams = queryParams;
            return this;
        }

        private AsyncRequest SetCallback(Action<string> callback)
        {
            _callback = callback;
            return this;
        }
    }
}
