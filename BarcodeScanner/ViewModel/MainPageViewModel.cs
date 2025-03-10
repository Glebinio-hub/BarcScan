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
        private Barcodes selectedItem;
        public static ObservableCollection<Barcodes> SelectedItems { get; set; }
        public Barcodes SelectedItem
        {
            get => selectedItem;
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    OnPropertyChanged(nameof(Barcodes));
                    UpdateSelectedItems();
                }
            }
        }
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
            SelectedItems = new ObservableCollection<Barcodes>();
            LoadBarcodes();
        }

        public void LoadBarcodes()
        {
            var FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DetectedBarcodes.txt");

            using (StreamReader Read = new StreamReader(FilePath))
            {
                Barcodes.Clear();
                while (Read.EndOfStream == false)
                {

                    string Json1 = Read.ReadLine();
                    var Barcode = JsonSerializer.Deserialize<Barcodes>(Json1);
                    Barcodes.Add(Barcode);

                }

                Read.Close();
            }
        }

        public void UpdateSelectedItems()
        {
            if (selectedItem != null && !SelectedItems.Contains(selectedItem))
            {
                SelectedItems.Add(selectedItem);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

