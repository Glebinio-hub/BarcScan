using BarcodeScanner.Model;
using System.Collections.ObjectModel;
using ZXing;
using ZXing.Net.Maui.Controls;
using System.Text.Json;
using System.ComponentModel;
using System.Drawing;

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

        ObservableCollection<Plants> CollectionOfPlants = new();

        var FilePath = Path.Combine(Directory, "DetectedBarcodes.txt");

        if (!File.Exists(FilePath))
        {
            using (StreamWriter Write = new(FilePath)) ;
        }

        using (StreamReader Read = new StreamReader(FilePath))
        {
            while (Read.EndOfStream == false)
            {
                string Json1 = Read.ReadLine();
                var Plant = JsonSerializer.Deserialize<Plants>(Json1);
                CollectionOfPlants.Add(Plant);
            }
        }

        using (StreamWriter Writer = new StreamWriter(FilePath))
        {
            if (CollectionOfPlants.Count < 10)
            {
                CollectionOfPlants.Add(new Plants
                {
                    Barcode = first.Value,
                    Articul = 128910404,
                    Description = "Это популярное растение",
                    Image = "tulp.jpg",
                    Name = "Тюльпан",
                    RetailPrice = 80,
                    WholesalePrice = 75
                });

                foreach (var Plant in CollectionOfPlants)
                {
                    string Json = JsonSerializer.Serialize(Plant);
                    Writer.WriteLine(Json);
                }
            }
            else
            {
                CollectionOfPlants.RemoveAt(CollectionOfPlants.Count - 9);
                CollectionOfPlants.Add(new Plants
                {
                    Barcode = first.Value,
                    Articul = 128910402,
                    Description = "Это расстение",
                    Image = "rose.jpg",
                    Name = "Роза",
                    RetailPrice = 250,
                    WholesalePrice = 200
                });

                foreach (var Plant in CollectionOfPlants)
                {
                    string Json = JsonSerializer.Serialize(Plant);
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
