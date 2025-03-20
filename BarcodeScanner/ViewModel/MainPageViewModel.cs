using Android.App.Usage;
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
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace BarcodeScanner.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {

        private RelayCommand loadInfoCommand;
        public RelayCommand LoadInfoCommand
        {
            get
            {
                return loadInfoCommand ??
                  (loadInfoCommand = new RelayCommand(obj =>
                  {
                      LoadBarcodes();
                  }));
            }
        }
        private ObservableCollection<Barcodes> barcodes;
        private Barcodes selectedItem;
        static string Directory = FileSystem.AppDataDirectory;
        string FilePath = Path.Combine(Directory, "DetectedBarcodes.txt");
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
            if (!File.Exists(FilePath))
            {
                using (StreamWriter Write = new(FilePath)) ;
            }

            using (StreamReader Read = new StreamReader(FilePath))
            {
                Barcodes.Clear();
                while (Read.EndOfStream == false)
                {
                    string Json1 = Read.ReadLine();
                    var Barcode = JsonSerializer.Deserialize<Barcodes>(Json1);
                    Barcodes.Add(Barcode);

                }
            }
        }

        public void UpdateSelectedItems()
        {
            if (selectedItem != null && !SelectedItems.Contains(selectedItem))
            {
                SelectedItems.Clear();
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

