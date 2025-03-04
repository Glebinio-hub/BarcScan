using BarcodeScanner.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BarcodeScanner.ViewModel
{
    public class MainPageViewModel:INotifyPropertyChanged
    {
        private ObservableCollection<Barcodes> barcodes;
        public ObservableCollection<Barcodes> Barcodes
        {
            get => barcodes;
            set
            {
                barcodes = value;
                OnPropertyChanged(nameof(Barcodes));
            }
        }
        public MainPageViewModel()
        {
            Barcodes = new ObservableCollection<Barcodes>();
            LoadBarcodes();
        }

        public void LoadBarcodes()
        {
            var FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DetectedBarcodes.txt");

            using (StreamReader Read = new StreamReader(FilePath))
            {
                //string line;
                Barcodes.Clear();
                //while ((line = Read.ReadLine()) != null)
                //{
                //    Barcodes.Add(new Barcodes { Barcode = line });
                //}
                while (Read.EndOfStream == false)
                {

                    string Json1 = Read.ReadLine();
                    var Barcode = JsonSerializer.Deserialize<Barcodes>(Json1);
                    Barcodes.Add(Barcode);

                }

                Read.Close();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

