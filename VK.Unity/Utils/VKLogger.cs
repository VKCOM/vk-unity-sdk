using UnityEngine;

namespace VK.Unity.Utils
{
    internal static class VKLogger
    {
        private static readonly IVKLogger _instance;

        static VKLogger()
        {
            _instance = new DebugLogger();
        }

        public static void Log(string message)
        {
            _instance.Log(message);
        }

        public static void Info(string message)
        {
            _instance.Info(message);
        }

        public static void Warn(string message)
        {
            _instance.Warn(message);
        }

        public static void Error(string message)
        {
            _instance.Error(message);
        }

        private class DebugLogger : IVKLogger
        {
            void IVKLogger.Log(string message)
            {
                if (Debug.isDebugBuild)
                {
                    Debug.Log(message);
                }
            }

            void IVKLogger.Info(string message)
            {
                Debug.Log(message);
            }

            void IVKLogger.Warn(string message)
            {
                Debug.LogWarning(message);
            }

            void IVKLogger.Error(string message)
            {
                Debug.LogError(message);
            }
        }
    }    
}
