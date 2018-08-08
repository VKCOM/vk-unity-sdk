using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace VK.Unity.Utils
{
    public static class VKUrlBuilder
    {
        public static string BuildAuthUrl(long appId, List<Scope> scope)
        {
            if (appId <= 0)
            {
                throw new ArgumentException("appId is incorrect");
            }

            const string version = VKContants.API_VERSION;
            const string display = "page";
            const string redirectUri = VKContants.AUTH_DEFAULT_REDIRECT_URI;
            const string responseType = "token";

            if (!scope.Contains(Scope.Offline))
            {
                scope.Add(Scope.Offline);
            }

            string scopeStr = string.Join(",", scope.Select(s => s.ToString().ToLowerInvariant()).ToArray());

            string authUrl = $"https://oauth.vk.com/authorize?client_id={appId}&display={display}&redirect_uri={redirectUri}&scope={scopeStr}&response_type={responseType}&v={version}";

            return authUrl;
        }

        public static string BuildRequestUrl(string method)
        {
            string url = string.Format(
                CultureInfo.InvariantCulture,
                VKContants.REQUEST_URL_FORMAT,
                method);

            return url;
        }
    }
}