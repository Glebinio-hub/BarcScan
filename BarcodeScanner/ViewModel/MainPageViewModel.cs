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
        private bool isLabelVisible;

        public bool IsLabelVisible
        {
            get => isLabelVisible;
            set
            {
                isLabelVisible = value;
                OnPropertyChanged(nameof(IsLabelVisible));
            }
        }


        private ObservableCollection<Plants> collectionOfPlants;
        private Plants selectedItem;
        static string Directory = FileSystem.AppDataDirectory;
        string FilePath = Path.Combine(Directory, "DetectedBarcodes.txt");
        public static ObservableCollection<Plants> SelectedItems { get; set; }
        public Plants SelectedItem
        {
            get => selectedItem;
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    OnPropertyChanged(nameof(Plants));
                    UpdateSelectedItems();
                }
            }
        }
        public ObservableCollection<Plants> CollectionOfPlants
        {
            get => collectionOfPlants;
            set
            {
                collectionOfPlants = value;
                OnPropertyChanged(nameof(Plants));
            }
        }
        public MainPageViewModel()
        {
            CollectionOfPlants = new ObservableCollection<Plants>();
            SelectedItems = new ObservableCollection<Plants>();
            LoadBarcodes();
        }


        public void LoadBarcodes()
        {
            if (!File.Exists(FilePath))
            {
                using (StreamWriter Write = new(FilePath)) ;
            }

            //using (StreamWriter Write = new(FilePath))
            //{
            //    Write.Write("");
            //}

            using (StreamReader Read = new StreamReader(FilePath))
            {
                IsLabelVisible = true;
                CollectionOfPlants.Clear();
                while (Read.EndOfStream == false)
                {
                    IsLabelVisible = false;
                    string Json1 = Read.ReadLine();
                    var Plant = JsonSerializer.Deserialize<Plants>(Json1);
                    CollectionOfPlants.Add(Plant);
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

