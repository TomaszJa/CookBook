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
        private string _searchQuery;
        private ObservableCollection<Recipe> _recipies;
        private RecipeType _recipeType;

        private INavigationService _navigationService;

        public ICommand RecipeSelectedCommand { get; }
        public ICommand SearchCommand { get; }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        public RecipeType RecipeType
        {
            get { return _recipeType; }
            set
            {
                _recipeType = value;
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

        public string SearchQuery
        {
            get { return _searchQuery; }
            set
            {
                _searchQuery = value;
                OnPropertyChanged("SearchQuery");
            }
        }

        public RecipiesListViewModel(INavigationService navigationService)
        {
            Recipies = new ObservableCollection<Recipe>();
            _navigationService = navigationService;

            RecipeSelectedCommand = new Command<Recipe>(OnRecipeSelectedCommand);
            SearchCommand = new Command(OnSearchCommand);
        }

        private async void OnSearchCommand()
        {
            CookBookDatabase database = await CookBookDatabase.Instance;
            var recipies = await database.GetRecipiesByNameAsync(SearchQuery, RecipeType);

            Recipies = new ObservableCollection<Recipe>(recipies);
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

        public override void Initialize(object parameter, List<Recipe> recipies, RecipeType type)
        {
            SearchQuery = "";
            Recipies = new ObservableCollection<Recipe>(recipies);
            Title = parameter as string;
            RecipeType = type;
        }
    }
}
