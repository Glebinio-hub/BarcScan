using Java.IO;
using ZXing;
using ZXing.Net.Maui.Controls;

namespace BarcodeScanner;

public partial class ScanPage : ContentPage
{
    private bool isScanningEnabled = true;
    public ScanPage()
    {
        InitializeComponent();
        cameraBarcodeReaderView.Options = new ZXing.Net.Maui.BarcodeReaderOptions
        {
            Formats = ZXing.Net.Maui.BarcodeFormat.Ean13,
            AutoRotate = true,
            Multiple = true
        };
    }

    protected void BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
        if (!isScanningEnabled) return;
        var first = e.Results?.FirstOrDefault();
        if (first is null)
        {
            return;
        }
        isScanningEnabled = false;

        List<string> Lines = new List<string>();
        var FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DetectedBarcodes.txt");

        //string FilePath = "C:\\MyProject\\BarcodeScanner\\BarcodeScanner\\Tools\\DetectedBarcodes.txt";


        using (StreamReader Read = new StreamReader(FilePath))
        {
            string line;

            while ((line = Read.ReadLine()) != null)
            {
                
                Lines.Add(line);
            }
        }

        using (StreamWriter Writer = new StreamWriter(FilePath))
        {
            if (Lines.Count <= 10)
            {
                foreach (string line1 in Lines)
                {
                    Writer.WriteLine(line1);
                }
                Writer.WriteLine(first.Value);
                
            }
            else
            {
                Lines.RemoveAt(Lines.Count - 1);
                foreach (string line1 in Lines)
                {
                    Writer.WriteLine(line1);
                }
                Writer.WriteLine(first.Value);
            }

            Dispatcher.DispatchAsync(async () =>
            {
                await DisplayAlert("Штрихкод Распознан", first.Value, "Сканировать еще");
                isScanningEnabled = true;
            });
        }
    }
}
        