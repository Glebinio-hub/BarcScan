namespace BarcodeScanner
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Routing.RegisterRoute("ScanPage", typeof(ScanPage));
            Routing.RegisterRoute("DetailedPage", typeof(DetailedPage));
            MainPage = new AppShell();


            
        }
    }
}
