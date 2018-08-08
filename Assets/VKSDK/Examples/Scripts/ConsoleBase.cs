
namespace VK.Unity.Example
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    internal class ConsoleBase : MonoBehaviour
    {
        private const int DpiScalingFactor = 160;
        private static Stack<string> menuStack = new Stack<string>();
        private string status = "Ready";
        private string lastResponse = string.Empty;
        private Vector2 scrollPosition = Vector2.zero;

        // DPI scaling
        private float? scaleFactor;
        private GUIStyle textStyle;
        private GUIStyle buttonStyle;
        private GUIStyle textInputStyle;
        private GUIStyle labelStyle;

        protected static int ButtonHeight
        {
            get
            {
                return 24;
            }
        }

        protected static int MainWindowWidth
        {
            get
            {
                return 700;
            }
        }

        protected static int MainWindowFullWidth
        {
            get
            {
                return 760;
            }
        }

        protected static int MarginFix
        {
            get
            {
                return 48;
            }
        }

        protected static Stack<string> MenuStack
        {
            get
            {
                return menuStack;
            }

            set
            {
                menuStack = value;
            }
        }

        protected string Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }

        protected Texture2D LastResponseTexture { get; set; }

        protected string LastResponse
        {
            get
            {
                return this.lastResponse;
            }

            set
            {
                this.lastResponse = value;
            }
        }

        protected Vector2 ScrollPosition
        {
            get
            {
                return this.scrollPosition;
            }

            set
            {
                this.scrollPosition = value;
            }
        }

       
        protected float ScaleFactor
        {
            get
            {
                if (!this.scaleFactor.HasValue)
                {
                    this.scaleFactor = Screen.dpi / ConsoleBase.DpiScalingFactor;
                }

                return this.scaleFactor.Value;
            }
        }

        protected int FontSize
        {
            get
            {
                return (int)Math.Round(this.ScaleFactor * 16);
            }
        }

        protected GUIStyle TextStyle
        {
            get
            {
                if (this.textStyle == null)
                {
                    this.textStyle = new GUIStyle(GUI.skin.textArea);
                    this.textStyle.alignment = TextAnchor.UpperLeft;
                    this.textStyle.wordWrap = true;
                    this.textStyle.padding = new RectOffset(10, 10, 10, 10);
                    this.textStyle.stretchHeight = true;
                    this.textStyle.stretchWidth = false;
                    this.textStyle.fontSize = this.FontSize;
                }

                return this.textStyle;
            }
        }

        protected GUIStyle ButtonStyle
        {
            get
            {
                if (this.buttonStyle == null)
                {
                    this.buttonStyle = new GUIStyle(GUI.skin.button);
                    this.buttonStyle.fontSize = this.FontSize;
                }

                return this.buttonStyle;
            }
        }

        protected GUIStyle TextInputStyle
        {
            get
            {
                if (this.textInputStyle == null)
                {
                    this.textInputStyle = new GUIStyle(GUI.skin.textField);
                    this.textInputStyle.fontSize = this.FontSize;
                }

                return this.textInputStyle;
            }
        }

        protected GUIStyle LabelStyle
        {
            get
            {
                if (this.labelStyle == null)
                {
                    this.labelStyle = new GUIStyle(GUI.skin.label);
                    this.labelStyle.fontSize = this.FontSize;
                }

                return this.labelStyle;
            }
        }

        protected virtual void Awake()
        {
            // Limit the framerate to 60 to keep device from burning through cpu
            Application.targetFrameRate = 60;
        }

        protected bool Button(string label)
        {
            return GUILayout.Button(
                label,
                this.ButtonStyle,
                GUILayout.MinHeight(ConsoleBase.ButtonHeight * this.ScaleFactor),
                GUILayout.MaxWidth(ConsoleBase.MainWindowWidth));
        }

        protected void LabelAndTextField(string label, ref string text)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(label, this.LabelStyle, GUILayout.MaxWidth(200 * this.ScaleFactor));
            text = GUILayout.TextField(
                text,
                this.TextInputStyle,
                GUILayout.MaxWidth(ConsoleBase.MainWindowWidth - 150));
            GUILayout.EndHorizontal();
        }

        protected bool IsHorizontalLayout()
        {
            #if UNITY_IOS || UNITY_ANDROID
            return Screen.orientation == ScreenOrientation.Landscape;
            #else
            return true;
            #endif
        }

        protected void SwitchMenu(Type menuClass)
        {
            ConsoleBase.menuStack.Push(this.GetType().Name);
            SceneManager.LoadScene(menuClass.Name);
        }

        protected void GoBack()
        {
            if (ConsoleBase.menuStack.Any())
            {
                SceneManager.LoadScene(ConsoleBase.menuStack.Pop());
            }
        }
    }
}
