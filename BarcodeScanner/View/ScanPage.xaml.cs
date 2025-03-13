using BarcodeScanner.Model;
using System.Collections.ObjectModel;
using ZXing;
using ZXing.Net.Maui.Controls;
using System.Text.Json;
using System.ComponentModel;

namespace BarcodeScanner;

public partial class ScanPage : ContentPage
{
    private bool isScanningEnabled = true;
    string Directory = FileSystem.AppDataDirectory;

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

        ObservableCollection<Barcodes> Barcodes = new();

        var FilePath = Path.Combine(Directory, "DetectedBarcodes.txt");

        if (!File.Exists(FilePath))
        {
            using (StreamWriter Write = new(FilePath)); 
        }

        using (StreamReader Read = new StreamReader(FilePath))
        {

            while (Read.EndOfStream == false)
            {
                string Json1 = Read.ReadLine();
                var Barcode = JsonSerializer.Deserialize<Barcodes>(Json1);
                Barcodes.Add(Barcode);
            }

        }

        using (StreamWriter Writer = new StreamWriter(FilePath))
        {

            if (Barcodes.Count< 10)
            {
                Barcodes.Add(new Barcodes { Barcode = first.Value});
                foreach ( var Barcode in Barcodes)
                {
                    string Json = JsonSerializer.Serialize(Barcode);
                    Writer.WriteLine(Json);
                    
                }           
            }

            else
            {
                Barcodes.RemoveAt(Barcodes.Count - 9);
                Barcodes.Add(new Barcodes { Barcode = first.Value });
                foreach (var Barcode in Barcodes)
                {
                    string Json = JsonSerializer.Serialize(Barcode);
                    Writer.WriteLine(Json);
                }
            }

            Dispatcher.DispatchAsync(async () =>
            {
                await DisplayAlert("Штрихкод Распознан", first.Value, "Сканировать еще");
                isScanningEnabled = true;
            });
        }
    }
}
        