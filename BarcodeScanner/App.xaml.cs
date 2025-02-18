namespace BarcodeScanner
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Routing.RegisterRoute("ScanPage", typeof(ScanPage));
            MainPage = new AppShell();
            
        }
    }
}
