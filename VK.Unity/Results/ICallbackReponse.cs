namespace VK.Unity.Results
{
    internal interface ICallbackReponse : IVKResponse
    {
        /// <summary>
        /// Gets the callback identifier.
        /// </summary>
        int CallbackId { get; }
    }
}
