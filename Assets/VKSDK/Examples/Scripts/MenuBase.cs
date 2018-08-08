
namespace VK.Unity.Example
{
    using Responses;
    using System.Linq;
    using UnityEngine;

    internal abstract class MenuBase : ConsoleBase
    {
        protected abstract string MenuName
        {
            get;
        }

        protected abstract void GetGui();

        protected virtual bool ShowDialogModeSelector()
        {
            return false;
        }

        protected virtual bool ShowBackButton()
        {
            return true;
        }

        protected void HandleResult(AuthResponse result)
        {
            if (result == null)
            {
                this.LastResponse = "Null Response\n";
               // LogView.AddLog(this.LastResponse);
                return;
            }

            this.LastResponseTexture = null;
            
            if (result.error != null)
            {
                this.Status = "Error - Check log for details";
                this.LastResponse = "Error Response:\n" + (result.error.errorMessage ?? "");
            }
            else 
            {
                this.Status = "Success - Check log for details";
                this.LastResponse = "Success Response:\n" + JsonUtility.ToJson(result);
            }
            
            //LogView.AddLog(result.ToString());
        }

        protected void OnGUI()
        {
            if (this.IsHorizontalLayout())
            {
                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical();
            }

            GUILayout.Label(MenuName, this.LabelStyle);

           

#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector2 scrollPosition = this.ScrollPosition;
                scrollPosition.y += Input.GetTouch(0).deltaPosition.y;
                this.ScrollPosition = scrollPosition;
            }
#endif
            this.ScrollPosition = GUILayout.BeginScrollView(
                this.ScrollPosition,
                GUILayout.MinWidth(ConsoleBase.MainWindowFullWidth));

            GUILayout.BeginHorizontal();
            if (this.ShowBackButton())
            {
                this.AddBackButton();
            }

       //     this.AddLogButton();
            if (this.ShowBackButton())
            {
                // Fix GUILayout margin issues
                GUILayout.Label(GUIContent.none, GUILayout.MinWidth(ConsoleBase.MarginFix));
            }

            GUILayout.EndHorizontal();
            if (this.ShowDialogModeSelector())
            {
            }

            GUILayout.BeginVertical();

            // Add the ui from decendants
            this.GetGui();
            GUILayout.Space(10);

            this.AddStatus();

            GUILayout.EndVertical();
            GUILayout.EndScrollView();
        }

        private void AddStatus()
        {
            GUILayout.Space(5);
            GUILayout.Box("Status: " + this.Status, this.TextStyle, GUILayout.MinWidth(ConsoleBase.MainWindowWidth), GUILayout.MaxHeight(500));
        }

        private void AddBackButton()
        {
            GUI.enabled = ConsoleBase.MenuStack.Any();
            if (this.Button("Back"))
            {
                this.GoBack();
            }

            GUI.enabled = true;
        }

        private void AddLogButton()
        {
            if (this.Button("Log"))
            {
           //     this.SwitchMenu(typeof(LogView));
            }
        }
    }
}
