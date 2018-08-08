namespace VK.Unity
{
    public class AccessToken
    {
        public string TokenString { get; private set; }

        public int ExpiresIn { get; private set; }
        
        public int UserId { get; private set; }

        internal static AccessToken Current { get;  set; }

        internal AccessToken(string tokenStr, int expiresIn, int userId)
        {
            TokenString = tokenStr;
            ExpiresIn = expiresIn;
            UserId = userId;
        }
    }
}
