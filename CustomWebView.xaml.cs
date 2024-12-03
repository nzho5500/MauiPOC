using Microsoft.Maui.Controls;

namespace test
{
    public partial class CustomWebView : WebView
    {
        public event Action<string> AddNumbersAction;
        public event Action<string> SubtractNumbersAction;

        public CustomWebView()
        {
            InitializeComponent();
        }

        public void OnAddNumbers(string data)
        {
            AddNumbersAction?.Invoke(data);
        }

        public void OnSubtractNumbers(string data)
        {
            SubtractNumbersAction?.Invoke(data);
        }
    }
}
