//using System;
//using System.Collections.Generic;
//using UnityEngine;
//using VK.Unity.Results;
//using VK.Unity.Utils;

//namespace VK.Unity.EditorImpl
//{
//    internal abstract class EditorDialog : MonoBehaviour
//    {
//        private Rect _clientRect;
//        private GUIStyle _style;

//        public Callback<VKResponseContainer> Callback { protected get; set; }

//        public int CallbackId { protected get; set; }

//        protected abstract string DialogTitle { get; }

//        public void Start()
//        {
//            _clientRect = new Rect(10, 10, Screen.width - 20, Screen.height - 20);
            
//            var texture = new Texture2D(1, 1);
//            texture.SetPixel(0, 0, new Color32(80, 113, 153, 255));
//            texture.Apply();

//            _style = new GUIStyle
//            {
//                normal =
//                {
//                    background = texture,
//                    textColor = Color.white
//                }
//            };
//        }

//        public void OnGUI()
//        {
//            GUI.ModalWindow(GetHashCode(), _clientRect, OnGUIDialog, DialogTitle, _style);
//        }

//        protected abstract void ComposeGUI();

//        protected abstract void SendSuccessResult();

//        protected virtual void SendCancelResult()
//        {
//            var dictionary = new Dictionary<string, object>
//            {
//                ["cancelled"] = true
//            };

//            if (CallbackId > 0)
//            {
//                dictionary[Constants.CALLBACK_ID_KEY] = CallbackId;
//            }

//            Callback?.Invoke(new VKResponseContainer(dictionary.ToJson()));
//        }

//        protected virtual void SendErrorResult(string errorMessage)
//        {
//            var dictionary = new Dictionary<string, object>
//            {
//                ["error"] = errorMessage
//            };

//            if (CallbackId > 0)
//            {
//                dictionary[Constants.CALLBACK_ID_KEY] = CallbackId;
//            }

//            Callback?.Invoke(new VKResponseContainer(dictionary.ToJson()));
//        }

//        private void OnGUIDialog(int windowId)
//        {
//            GUILayout.Space(20);

//            GUILayout.BeginVertical();
//            ComposeGUI();
//            GUILayout.EndVertical();

//            GUILayout.BeginHorizontal();
//            GUILayout.FlexibleSpace();

//            CreateButton("Success", SendSuccessResult);
//            CreateButton("Cancel", SendCancelResult);
//            CreateButton("Error", () => SendErrorResult("Error button pressed"));

//            GUILayout.EndHorizontal();
//        }

//        private void CreateButton(string caption, Action callback)
//        {
//            var label = new GUIContent(caption);
//            var buttonRect = GUILayoutUtility.GetRect(label, GUI.skin.button);

//            if (GUI.Button(buttonRect, label))
//            {
//                callback?.Invoke();

//                Destroy(this);
//            }
//        }
//    }
//}
