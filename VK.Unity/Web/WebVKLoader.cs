namespace VK.Unity.Web
{
    class WebVKLoader : VKSDK.VKLoader
    {
        protected override VKGameManager CreateManager()
        {
            var WebVKGameManagerComponent = ComponentFactory.GetComponent<WebVKGameManager>();
            if (WebVKGameManagerComponent.Client == null)
            {
                WebVKGameManagerComponent.Client = new WebVKClient();
            }
            return WebVKGameManagerComponent;
        }
    }
}