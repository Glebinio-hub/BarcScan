using BarcodeScanner.ViewModel;

namespace BarcodeScanner;

public partial class DetailedPage : ContentPage
{
	public DetailedPage()
	{
		InitializeComponent();
		BindingContext = new DetailedPageViewModel();

    }
}