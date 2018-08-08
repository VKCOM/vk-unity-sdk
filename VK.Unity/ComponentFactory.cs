using UnityEngine;

namespace VK.Unity
{
    internal class ComponentFactory
    {
        private const string GAME_OBJECT_NAME = "VKUnityPlugin";

        private static GameObject _gameObject;

        internal enum IfNotExist
        {
            AddNew,
            ReturnNull
        }

        private static GameObject GameObject => _gameObject ?? (_gameObject = new GameObject(GAME_OBJECT_NAME));

        /// <summary>
        /// Gets one and only one component. Lazy creates one if it doesn't exist
        /// </summary>
        public static T GetComponent<T>(IfNotExist ifNotExist = IfNotExist.AddNew) where T : MonoBehaviour
        {
            GameObject gameObject = GameObject;

            T component = gameObject.GetComponent<T>();
            if (component == null && ifNotExist == IfNotExist.AddNew)
            {
                component = gameObject.AddComponent<T>();
            }

            return component;
        }

        /// <summary>
        /// Creates a new component on the VK object regardless if there is already one
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T AddComponent<T>() where T : MonoBehaviour
        {
            return GameObject.AddComponent<T>();
        }
    }
}
