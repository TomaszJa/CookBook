using CookBook.Models;
using CookBook.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CookBook.ViewModels.Recipies
{
    public class RecipiesListViewModel : BaseViewModel
    {
        private string _title;
        private ObservableCollection<Recipe> _recipies;

        private INavigationService _navigationService;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        public ObservableCollection<Recipe> Recipies
        {
            get => _recipies;
            set
            {
                _recipies = value;
                OnPropertyChanged("Recipies");
            }
        }

        public RecipiesListViewModel(INavigationService navigationService)
        {
            Recipies = new ObservableCollection<Recipe>();
            _navigationService = navigationService;

        }

        public override void Initialize(object parameter)
        {
            Title = parameter as string;
        }

        public override void Initialize(object parameter, List<Recipe> recipies)
        {
            Title = parameter as string;

            foreach ( var recipe in recipies )
            {
                Recipies.Add(recipe);
            }
        }
    }
}
