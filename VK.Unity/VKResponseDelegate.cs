using VK.Unity.Results;

namespace VK.Unity
{
    /// <summary>
    /// VK response delegate
    /// </summary>
    /// <typeparam name="T">The response type.</typeparam>
    /// <param name="response">The response.</param>
    public delegate void VKResponseDelegate<in T>(T response) where T : IVKResponse;
}
