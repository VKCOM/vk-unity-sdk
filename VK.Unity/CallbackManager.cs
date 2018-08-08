using System;
using System.Collections.Generic;
using VK.Unity.Responses;

namespace VK.Unity
{
    class CallbackManager
    {
        private readonly Dictionary<int, object> callbackDict = new Dictionary<int, object>();
        private int _nextCallbackId = 1;

        public int RegisterCallback<T>(Action<T> callback) where T: ResponseBase
        {
            if (callback == null)
            {
                return 0;
            }

            callbackDict.Add(_nextCallbackId, callback);
            _nextCallbackId++;
            return _nextCallbackId - 1;
        }

        public void RemoveCallback(int callbackId)
        {
            callbackDict.Remove(callbackId);
        }

        public void HandleResponse(ResponseBase response)
        {
            if (string.IsNullOrEmpty(response.callbackId))
            {
                return;
            }

            int callbackIdInt = 0;


            object callback;
            if (int.TryParse(response.callbackId, out callbackIdInt) &&
                callbackDict.TryGetValue(callbackIdInt, out callback))
            {
                CallCallback(callback, response);
                callbackDict.Remove(callbackIdInt);
            }
        }

        public void HandleCallback(ResponseBase response)
        {
            if (string.IsNullOrEmpty(response.callbackId))
            {
                return;
            }

            int callbackIdInt = 0;

            object callback;
            if (int.TryParse(response.callbackId, out callbackIdInt) &&
                callbackDict.TryGetValue(callbackIdInt, out callback))
            {
                CallCallback(callback, response);
            }
        }

        private void CallCallback(object callback, ResponseBase response)
        {
            if (callback == null || response == null)
            {
                return;
            }

            TryCallCallback<APICallResponse>(callback, response);
            // to do : add other types, when needed
        }

        private bool TryCallCallback<T>(object callback, ResponseBase response) where T : ResponseBase
        {
            var castedCallback = callback as Action<T>;
            if (castedCallback != null)
            {
                castedCallback((T)response);
                return true;
            }
            return false;
        }
    }



    //internal class VKCallbackManager
    //{
    //    private readonly IDictionary<int, object> _callbacks = new Dictionary<int, object>();
    //    private int _currentCallbackId;

    //    public int AddCallback<T>(VKResponseDelegate<T> callback) where T : IVKResponse
    //    {
    //        if (callback == null)
    //        {
    //            return -1;
    //        }

    //        _currentCallbackId++; 
    //        _callbacks.Add(_currentCallbackId, callback);
    //        return _currentCallbackId;
    //    }

    //    public void HandleResponse(ICallbackReponse response)
    //    {
    //        int callbackId = response?.CallbackId ?? -1;

    //        if (callbackId < 0)
    //        {
    //            return;
    //        }

    //        object callback;
    //        if (_callbacks.TryGetValue(callbackId, out callback))
    //        {
    //            CallCallback(callback, response);
    //            _callbacks.Remove(callbackId);
    //        }
    //    }

    //    // Since unity mono doesn't support covariance and contravariance use this hack
    //    private static void CallCallback(object callback, IVKResponse response)
    //    {
    //        if (callback == null || response == null)
    //        {
    //            return;
    //        }

    //        if (TryCallCallback<ILoginResponse>(callback, response))
    //        {
    //            return;
    //        }

    //        throw new NotSupportedException("Unexpected result type: " + callback.GetType().FullName);
    //    }

    //    private static bool TryCallCallback<T>(object callback, IVKResponse response) where T : IVKResponse
    //    {
    //        var castedCallback = callback as VKResponseDelegate<T>;
    //        if (castedCallback != null)
    //        {
    //            castedCallback((T)response);
    //            return true;
    //        }

    //        return false;
    //    }
    //}
}
