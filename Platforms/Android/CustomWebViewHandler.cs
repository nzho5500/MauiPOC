using Android.Webkit;
using Java.Interop;
using Microsoft.Maui.Handlers;
using test;

namespace test
{
    public class CustomWebViewHandler : WebViewHandler
    {
        protected override void ConnectHandler(Android.Webkit.WebView platformView)
        {
            base.ConnectHandler(platformView);

            // Enable JavaScript
            platformView.Settings.JavaScriptEnabled = true;

            // Add JavaScript interface
            platformView.AddJavascriptInterface(new JSBridge((CustomWebView)VirtualView), "CSharp");
        }

        protected override void DisconnectHandler(Android.Webkit.WebView platformView)
        {
            platformView.RemoveJavascriptInterface("CSharp");
            base.DisconnectHandler(platformView);
        }
    }

    public class JSBridge : Java.Lang.Object
    {
        readonly CustomWebView _customWebView;

        public JSBridge(CustomWebView customWebView)
        {
            _customWebView = customWebView;
        }

        // Separate methods for each JavaScript action
        [JavascriptInterface]
        [Export("addNumbers")]
        public void AddNumbers(string data)
        {
            _customWebView.OnAddNumbers(data);
        }

        [JavascriptInterface]
        [Export("subtractNumbers")]
        public void SubtractNumbers(string data)
        {
            _customWebView.OnSubtractNumbers(data);
        }
    }
}
