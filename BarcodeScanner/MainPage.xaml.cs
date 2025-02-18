using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;
using System.Windows;

namespace BarcodeScanner
{
    public partial class MainPage : ContentPage
    {
        public List<string> Barcodes { get; set; }

        public MainPage()
        {
            InitializeComponent();

            var FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DetectedBarcodes.txt");
            Barcodes = new List<string>();
            using (StreamReader Read = new StreamReader(FilePath))
            {
                string line;
                while ((line = Read.ReadLine()) != null)
                {
                    Barcodes.Add(line);
                }
            }
            BindingContext = this;
        }

        private async void ScanButton_Click(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("ScanPage");
        }
}
}
