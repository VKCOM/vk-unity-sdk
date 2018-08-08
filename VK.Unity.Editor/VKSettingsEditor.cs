using System.IO;
using UnityEditor;
using UnityEngine;

namespace VK.Unity.Editor
{
    [InitializeOnLoad]
    [CustomEditor(typeof(VKSettings))]
    public class VKSettingsEditor : UnityEditor.Editor
    {
        private readonly GUIContent _appIdLabel = new GUIContent("Application ID [?]:",
            "Your VK app id (https://vk.com/apps?act=manage)");

        private readonly GUIContent _apiVersionLabel = new GUIContent("API version (optional) [?]:",
            "Optional version of the VK API to be used. More information: https://vk.com/dev/versions");

        private GUIContent packageNameLabel = new GUIContent("Package Name [?]", "Bundle identifier");

        public VKSettingsEditor()
        {
            VKSettings.RegisterChangeCallback(SettingsChanged);
        }

        private void SettingsChanged()
        {
            EditorUtility.SetDirty((VKSettings)target);
        }

        [MenuItem("VK/Edit Settings")]
        public static void EditSettings()
        {
            var instance = VKSettings.NullableInstance;

            if (instance == null)
            {
                instance = ScriptableObject.CreateInstance<VKSettings>();
                string properPath = Path.Combine(Application.dataPath, VKSettings.PATH);
                if (!Directory.Exists(properPath))
                {
                    Directory.CreateDirectory(properPath);
                }

                string fullPath = Path.Combine(
                                      Path.Combine("Assets", VKSettings.PATH),
                                      VKSettings.ASSET_NAME + VKSettings.ASSET_EXTENSION);
                AssetDatabase.CreateAsset(instance, fullPath);
            }

            Selection.activeObject = VKSettings.Instance;
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.Separator();
            CreateAppIdGUI();          
        }

        private void CreateAppIdGUI()
        {
            EditorGUILayout.LabelField("Add the VK Application ID associated with this game");
            if (VKSettings.AppId <= 0)
            {
                EditorGUILayout.HelpBox("Invalid Application ID", MessageType.Error);
            }

            EditorGUILayout.LabelField(_appIdLabel);

            long appId;
            string appIdStr = "";

            appIdStr = EditorGUILayout.TextField(VKSettings.AppId.ToString());

            if (long.TryParse(appIdStr, out appId))
            {
                VKSettings.AppId = appId;
            }


            EditorGUILayout.LabelField(_apiVersionLabel);
            
            string apiVersionStr = "";

            apiVersionStr = EditorGUILayout.TextField(VKSettings.ApiVersion);
            
            VKSettings.ApiVersion = apiVersionStr;

            this.SelectableLabelField(this.packageNameLabel, PlayerSettings.bundleIdentifier);
        }

        private void SelectableLabelField(GUIContent label, string value)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, GUILayout.Width(180), GUILayout.Height(16));
            EditorGUILayout.SelectableLabel(value, GUILayout.Height(16));
            EditorGUILayout.EndHorizontal();
        }
    }
}
