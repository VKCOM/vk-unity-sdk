using System.Collections.Generic;

namespace VK.Unity.Utils
{
    public static class VKRequestUtils
    {
        /// <summary>
        /// Adds API version query parameter if possible
        /// </summary>
        /// <param name="queryParams">Query parameters dictionary</param>
        public static void AddApiVersion(this IDictionary<string, string> queryParams)
        {
            if (queryParams == null)
            {
                return;
            }

            const string apiVersionKey = VKRequestKeys.API_VERSION_KEY;
            if (!queryParams.ContainsKey(apiVersionKey))
            {
                queryParams[apiVersionKey] = VKContants.API_VERSION;
            }
        }

        /// <summary>
        /// Adds Access Token query parameter if possible
        /// </summary>
        /// <param name="queryParams">Query parameters dictionary</param>
        public static void AddAccessToken(this IDictionary<string, string> queryParams)
        {
            if (queryParams == null)
            {
                return;
            }

            const string accessTokenKey = VKRequestKeys.ACCESS_TOKEN_KEY;

            string accessToken = AccessToken.Current.TokenString;

            if (!queryParams.ContainsKey(accessTokenKey) && !string.IsNullOrEmpty(accessToken))
            {
                queryParams[accessTokenKey] = accessToken;
            }
        }
    }
}