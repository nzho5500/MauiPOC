using Microsoft.Maui.Controls;
using System.Diagnostics;
using System.Text.Json;

namespace test
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            CustomWebView.AddNumbersAction += HandleAddNumbers;
            CustomWebView.SubtractNumbersAction += HandleSubtractNumbers;

            var htmlSource = new HtmlWebViewSource
            {
                Html = @"
                    <!DOCTYPE html>
                    <html>
                    <head>
                        <script>
                            function addNumbers() {
                                const num1 = 10;
                                const num2 = 5;
                                window.CSharp.addNumbers(JSON.stringify({ Number1: num1, Number2: num2 }));
                            }

                            function subtractNumbers() {
                                const num1 = 10;
                                const num2 = 5;
                                window.CSharp.subtractNumbers(JSON.stringify({ Number1: num1, Number2: num2 }));
                            }
                        </script>
                    </head>
                    <body>
                        <p id='resultLabel'>Result will be displayed here.</p>
                        <button onclick='addNumbers()'>Add Numbers</button>
                        <button onclick='subtractNumbers()'>Subtract Numbers</button>
                    </body>
                    </html>"
            };
            CustomWebView.Source = htmlSource;

        }

        private void HandleAddNumbers(string data)
        {
            var parsedData = JsonSerializer.Deserialize<CalculationInput>(data);
            if (parsedData != null)
            {
                int result = parsedData.Number1 + parsedData.Number2;

                // Update the label text with the addition result
                CustomWebView.EvaluateJavaScriptAsync($"document.getElementById('resultLabel').innerText = 'Addition Result: {result}';");
            }
        }

        private void HandleSubtractNumbers(string data)
        {
            var parsedData = JsonSerializer.Deserialize<CalculationInput>(data);
            if (parsedData != null)
            {
                int result = parsedData.Number1 - parsedData.Number2;

                // Update the label text with the subtraction result
                CustomWebView.EvaluateJavaScriptAsync($"document.getElementById('resultLabel').innerText = 'Subtraction Result: {result}';");
            }
        }
    }

    public class CalculationInput
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
    }
}
