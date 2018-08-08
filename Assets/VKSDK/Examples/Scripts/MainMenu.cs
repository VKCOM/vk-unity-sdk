
namespace VK.Unity.Example
{
    using VK.Unity;
    using System.Collections.Generic;
    using UnityEngine;

    using Responses;
    using System;

    internal sealed class MainMenu : MenuBase
    {
        protected override string MenuName
        {
            get
            {
                return "VK Unity SDK Test Menu";
            }
        }

        protected override bool ShowBackButton()
        {
            return false;
        }

        protected override void GetGui()
        {
            bool enabled = GUI.enabled;
            if (this.Button("VK.Init"))
            {
                try
                {
                    VKSDK.Init(OnInitComplete);
                    this.Status = "VK.Init() called with " + VKSDK.AppId;
                }
                catch (Exception exc)
                {
                    Status = "VK.Init faled: " + exc.Message;
                }

                VKSDK.OnAccessTokenChanged = (at) =>
                {
                    this.Status += Environment.NewLine + " AccessToken changed!";
                };                
            }
         
            GUI.enabled = enabled && VKSDK.IsInitialized;
            if (this.Button("Login"))
            {
                this.CallLogin();
                this.Status = "Login called";
            }

            GUI.enabled = enabled && VKSDK.IsLoggedIn;
            if (this.Button("Logout"))
            {
                CallLogout();
                Status = "Logout called";
            }
         
            GUI.enabled = enabled && VKSDK.IsLoggedIn;
            if (this.Button("API call (user.get)"))
            {
                CallApi();
                Status = "API.user.get is called";
            }

            if (Button("API call (friends.get)"))
            {
                CallGetFriends();
                Status = "API.friends.get is called";
            }

            if (Button("Test validation"))
            {
                TestValidation();
                Status = "API.account.testValidation is called";
            }

            GUI.enabled = enabled;
        }


        private void CallGetFriends()
        {
            VKSDK.API("friends.get", new Dictionary<string, string>() { { "order", "hints" } }  , (result) =>
            {
                Status = "API.friends.get call completed: " + result.ToString();
            });
        }

        private void TestValidation()
        {
            VKSDK.API("account.testValidation", new Dictionary<string, string>(), (result) =>
            {
                Status = "API.account.testValidation call completed: " + result.ToString();
            });
        }

        private void CallLogout()
        {
            VKSDK.Logout();
        }

        private void CallApi()
        {
            VKSDK.API("users.get", new Dictionary<string, string>(), (result) =>
            {
                Status = "API.users.get call completed: " + result.ToString();
            });
        }

        private void CallLogin()
        {
            VKSDK.Login(new List<Scope> {Scope.Friends, Scope.Offline}, OnLoginCompleted);
        }

        private void OnLoginCompleted(AuthResponse response)
        {
            Status = "Login completed: " + JsonUtility.ToJson(response);
        }

        private void OnInitComplete()
        {
            Status = "Init completed: SDK version =  " + VKSDK.SDKVersion + ", appId = " + VKSDK.AppId + ", isLoggedIn = " + VKSDK.IsLoggedIn;

#if UNITY_ANDROID
            Status += ", cert. fingerprint= " + VKSDK.GetExtraData(VKSDK.EXTRA_ANDROID_CERTIFICATE_FINGERPRINT_KEY);
#endif
        }
    }
}
