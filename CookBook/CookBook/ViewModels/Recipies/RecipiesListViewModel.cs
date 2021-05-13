using CookBook.Data;
using CookBook.Models;
using CookBook.Services.Interfaces;
using CookBook.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CookBook.ViewModels.Recipies
{
    public class RecipiesListViewModel : BaseViewModel
    {
        private string _title;
        private ObservableCollection<Recipe> _recipies;

        private INavigationService _navigationService;

        public ICommand RecipeSelectedCommand { get; }

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

            RecipeSelectedCommand = new Command<Recipe>(OnRecipeSelectedCommand);
        }

        private void OnRecipeSelectedCommand(Recipe recipe)
        {
            // Z jakiegoś powodu Initialize nie aktualizuje obiektów w dzieciach tabbed page
            // dlatego inicjalizuję je tutaj.
            ViewModelLocator.RecipeDetailsViewModel.Initialize(recipe);
            ViewModelLocator.RecipeIngredientsViewModel.Initialize(recipe);
            _navigationService.NavigateTo(ViewNames.RecipiesTabbedView, recipe);

            MessagingCenter.Subscribe<RecipeDetailsViewModel, Recipe>
                (this, MessageNames.RecipeChangedMessage, OnRecipeChanged);
        }

        private async void OnRecipeChanged(RecipeDetailsViewModel sender, Recipe recipe)
        {
            CookBookDatabase database = await CookBookDatabase.Instance;
            var recipies = await database.GetRecipiesByTypeAsync(recipe.Type);

            Recipies = new ObservableCollection<Recipe>(recipies);
        }

        public override void Initialize(object parameter)
        {
            Title = parameter as string;
        }

        public override void Initialize(object parameter, List<Recipe> recipies)
        {
            Recipies = new ObservableCollection<Recipe>();
            Title = parameter as string;

            foreach ( var recipe in recipies )
            {
                Recipies.Add(recipe);
            }
        }
    }
}
