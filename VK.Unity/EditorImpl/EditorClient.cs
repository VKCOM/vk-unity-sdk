//using System.Collections.Generic;
//using VK.Unity.Results;

//namespace VK.Unity.EditorImpl
//{
//    internal class EditorClient : VKClientBase
//    {
//        private readonly IEditorWrapper _editorWrapper;

//        public EditorClient(IEditorWrapper editorWrapper, VKCallbackManager callbackManager) : base(callbackManager)
//        {
//            _editorWrapper = editorWrapper;
//        }

//        public override void LogIn(IEnumerable<Scope> scope, VKResponseDelegate<ILoginResponse> callback = null)
//        {
//            int callbackId = CallbackManager.AddCallback(callback);
//            _editorWrapper.ShowLoginDialog(OnLoginComplete, callbackId);
//        }

//        public override void OnLoginComplete(VKResponseContainer container)
//        {
//            var result = new LoginResponse(container);
//            HandleLoginComplete(result);
//        }
//    }
//}
