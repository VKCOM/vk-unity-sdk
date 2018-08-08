using UnityEngine;
using VK.Unity.Results;

namespace VK.Unity
{
    internal abstract class VKGameManager : MonoBehaviour
    {
        public IVKClientWithResultHandler Client { get; set; }

        public void Awake()
        {
            DontDestroyOnLoad(this);

            OnAwake();
        }

        public void OnInitComplete(string message)
        {
            Client.OnInitComplete(message);
        }

        public void OnLoginComplete(string message)
        {
            Client.OnLoginComplete(message);
        }

        public void OnAPICallComplete(string message)
        {
            Client.OnAPICallComplete(message);
        }

        public void OnAccessTokenChanged(string message)
        {
            Client.OnAccessTokenChanged(message);
        }
 
        protected virtual void OnAwake()
        {
        }
    }
}
