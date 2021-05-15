using SQLite;
using System.ComponentModel;

namespace CookBook.Models
{
    public class ShoppingListItem : INotifyPropertyChanged
    {
        private string _name;
        private bool _check = false;

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        public bool Check
        {
            get => _check;
            set
            {
                _check = value;
                RaisePropertyChanged(nameof(Check));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
