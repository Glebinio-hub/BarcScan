using BarcodeScanner.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeScanner.ViewModel;


public class DetailedPageViewModel : INotifyPropertyChanged
{
   public ObservableCollection<Plants> SelectedItem { get; set; }

    public DetailedPageViewModel()
    {
        SelectedItem = new();
        
        foreach (var item in MainPageViewModel.SelectedItems)
        {
           SelectedItem.Clear(); 
           SelectedItem.Add(item);

        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
