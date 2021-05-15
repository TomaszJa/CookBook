using SQLite;
using System;
using System.ComponentModel;

namespace CookBook.Models
{
    public class Recipe : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        private string _name;
        private RecipeType _type = RecipeType.Other;
        private string _ingredients;
        private string _description;
        private int _preparationTime;
        // Nie działa automatyczna konwersja stringa na URL
        private string _stringURL;
        private Uri _uRL;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        public RecipeType Type
        {
            get => _type;
            set
            {
                _type = value;
                RaisePropertyChanged(nameof(Type));
            }
        }
        public string Ingredients
        {
            get => _ingredients;
            set
            {
                _ingredients = value;
                RaisePropertyChanged(nameof(Ingredients));
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }
        public int PreparationTime
        {
            get => _preparationTime;
            set
            {
                _preparationTime = value;
                RaisePropertyChanged(nameof(PreparationTime));
            }
        }
        // Nie działa automatyczna konwersja stringa na URL
        public string StringURL
        {
            get => _stringURL;
            set
            {
                _stringURL = value;
                RaisePropertyChanged(nameof(StringURL));
            }
        }
        public Uri URL
        {
            get => _uRL;
            set
            {
                _uRL = value;
                RaisePropertyChanged(nameof(URL));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
