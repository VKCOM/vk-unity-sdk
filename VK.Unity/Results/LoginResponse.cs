namespace VK.Unity.Results
{
    internal class LoginResponse : ResponseBase, ILoginResponse
    {
        public LoginResponse(VKResponseContainer container) : base(container)
        {
            var resultDictionary = container.ResultDictionary;
            if (resultDictionary != null && resultDictionary.ContainsKey(Constants.ACCESS_TOKEN_KEY))
            {
                AccessToken = resultDictionary[Constants.ACCESS_TOKEN_KEY].ToString();
            }
        }

        public string AccessToken { get; }

        public override string ToString()
        {
            return "access_token: " + AccessToken;
        }
    }
}
