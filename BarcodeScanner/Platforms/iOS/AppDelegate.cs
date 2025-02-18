using Foundation;
using ZXing.Net.Maui;

namespace BarcodeScanner
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.Create();
    }
}
