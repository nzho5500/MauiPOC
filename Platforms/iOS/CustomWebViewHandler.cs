using Foundation;
using Microsoft.Maui.Handlers;
using WebKit;
using test;

namespace test
{
    public class CustomWebViewHandler : WebViewHandler
    {
        protected override WKWebView CreatePlatformView()
        {
            // Set up the configuration and user content controller
            var config = new WKWebViewConfiguration();
            var userController = new WKUserContentController();

            // Add JavaScript message handlers
            userController.AddScriptMessageHandler(new JSBridge((CustomWebView)VirtualView), "addNumbers");
            userController.AddScriptMessageHandler(new JSBridge((CustomWebView)VirtualView), "subtractNumbers");

            config.UserContentController = userController;

            // Create WKWebView with the appropriate frame
            var platformView = new WKWebView(CoreGraphics.CGRect.Empty, config);

            return platformView;
        }

        protected override void DisconnectHandler(WKWebView platformView)
        {
            if (platformView?.Configuration.UserContentController != null)
            {
                platformView.Configuration.UserContentController.RemoveAllUserScripts();
                platformView.Configuration.UserContentController.RemoveScriptMessageHandler("addNumbers");
                platformView.Configuration.UserContentController.RemoveScriptMessageHandler("subtractNumbers");
            }

            base.DisconnectHandler(platformView);
        }
    }

    public class JSBridge : NSObject, IWKScriptMessageHandler
    {
        private readonly CustomWebView _customWebView;

        public JSBridge(CustomWebView customWebView)
        {
            _customWebView = customWebView;
        }

        [Export("userContentController:didReceiveScriptMessage:")]
        public void DidReceiveScriptMessage(WKUserContentController userContentController, WKScriptMessage message)
        {
            if (message.Name == "addNumbers")
            {
                _customWebView.OnAddNumbers(message.Body?.ToString());
            }
            else if (message.Name == "subtractNumbers")
            {
                _customWebView.OnSubtractNumbers(message.Body?.ToString());
            }
        }
    }
}
