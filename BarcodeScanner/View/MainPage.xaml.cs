using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;
using System.Windows;
using BarcodeScanner.ViewModel;

namespace BarcodeScanner
{
    public partial class MainPage : ContentPage
    {
        public List<string> Barcodes { get; set; }

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }

        private async void ScanButton_Click(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("ScanPage");
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((MainPageViewModel)this.BindingContext).LoadBarcodes();
        }
    }
}
