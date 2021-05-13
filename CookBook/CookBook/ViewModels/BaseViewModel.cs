using CookBook.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CookBook.ViewModels
{
    // Każdy ViewModel musi implementować interfejs INotifyPropertyChanged
    // żeby sygnalizować (Notify) zmianę obiektu. Dlatego tworzę tę klasę
    // bazową, żeby nie implementować wielokrotnie tego samego kodu
    public class BaseViewModel : INotifyPropertyChanged
    {
        // Potrzebny do wykorzystania Binding Mode 
        // event raised gdy atrybut obiektu ulegnie zmianie
        public event PropertyChangedEventHandler PropertyChanged;

        // Sprawdzenie, czy atrybut uległ zmianie i nie jest nullem
        // jeżeli nie jest to utworzony zostanie nowy event z przekazanym
        // atrybutem
        public void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // 
        public virtual void Initialize(object parameter)
        {

        }

        public virtual void Initialize(object parameter, List<Recipe> recipies)
        {

        }

        public virtual void Initialize(object parameter, List<ShoppingListItem> shoppingListItems)
        {

        }
    }
}
