namespace VK.Unity.Utils
{
    internal interface IVKLogger
    {
        void Log(string message);

        void Info(string message);

        void Warn(string message);

        void Error(string message);
    }
}
