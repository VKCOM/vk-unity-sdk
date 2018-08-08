//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;
//using VK.Unity.Results;
//using VK.Unity.Utils;

//namespace VK.Unity.EditorImpl.Dialogs
//{
//    internal class LoginDialog : EditorDialog
//    {
//        private string _accessToken = "";

//        protected override string DialogTitle => "Login";

//        protected override void ComposeGUI()
//        {
//            GUILayout.BeginHorizontal();
//            GUILayout.Label("User Access Token:");
//            _accessToken = GUILayout.TextField(_accessToken, GUI.skin.textArea, GUILayout.MinWidth(100));
//            GUILayout.EndHorizontal();

//            GUILayout.Space(10);

//            if (GUILayout.Button("Get access token"))
//            {
//                GetAccessToken();
//            }
//        }

//        private static void GetAccessToken()
//        {
//            long appId = VKSDK.AppId;
//            if (appId <= 0)
//            {
//                return;
//            }

//            string authUrl = VKUrlBuilder.BuildAuthUrl(appId, new List<Scope> { Scope.Friends });
//            Application.OpenURL(authUrl);
//        }

//        protected override void SendSuccessResult()
//        {
//            if (string.IsNullOrEmpty(_accessToken))
//            {
//                SendErrorResult("Empty access token");
//            }

//            ValidateAccessToken();
//        }

//        private void ValidateAccessToken()
//        {
//            const string method = "users.get";
//            var queryParams = new Dictionary<string, string>
//            {
//                ["user_id"] = "6205753"
//            };

//            VKSDK.API(method, queryParams, jsonString =>
//            {
//                var resultDictionary = (IDictionary<string, object>)Json.Deserialize(jsonString);                

//                if (resultDictionary == null || !resultDictionary.ContainsKey(Constants.RESPONSE_KEY))
//                {
//                    SendErrorResult("Access token is incorrect");
//                    return;
//                }

//                var response = (resultDictionary[Constants.RESPONSE_KEY] as List<object>)?.FirstOrDefault() as IDictionary<string, object>; 
//                if (response == null)
//                {
//                    SendErrorResult("Auth response is invalid");
//                    return;
//                }

//                if (CallbackId > 0)
//                {
//                    response[Constants.CALLBACK_ID_KEY] = CallbackId;
//                }

//                response[Constants.ACCESS_TOKEN_KEY] = _accessToken;

//                Callback?.Invoke(new VKResponseContainer(response));
//            });
//        }
//    }
//}
