using VK.Unity.Results;

namespace VK.Unity
{
    internal interface IVKClientResponseHandler
    {
        void OnInitComplete(string responseStr);
        void OnLoginComplete(string responseStr);

        void OnAPICallComplete(string responseStr);

        void OnAccessTokenChanged(string messageStr);
    }
}
