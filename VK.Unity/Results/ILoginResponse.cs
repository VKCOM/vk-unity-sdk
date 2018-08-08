namespace VK.Unity.Results
{
    public interface ILoginResponse : IVKResponse
    {
        string AccessToken { get; }
    }
}
