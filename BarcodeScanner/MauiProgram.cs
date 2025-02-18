using Microsoft.Extensions.Logging;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;

namespace BarcodeScanner
{
    public static class MauiProgram
    {
        public static MauiApp Create()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .UseBarcodeReader(); // This line initializes the barcode reader plugin

            return builder.Build();
        }

    }
}
