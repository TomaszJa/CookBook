using CookBook.Data;
using CookBook.Models;
using CookBook.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CookBook.ViewModels.Recipies
{
    public class RecipeCreateViewModel : BaseViewModel
    {
        private Recipe _recipe;
        private List<string> _types = new List<string>()
        {
            "Breakfast", "Starter", "Soup", "Main Course", "Dessert", "Other"
        };
        private string _chosenValue;
        private INavigationService _navigationService;

        public Recipe RecipeToCreate
        {
            get { return _recipe; }
            set
            {
                _recipe = value;
                OnPropertyChanged("SelectedErrand");
            }
        }
        public List<string> Types
        {
            get { return _types; }
            set
            {
                _types = value;
                OnPropertyChanged("Type");
            }
        }

        public string ChosenValue
        {
            get { return _chosenValue; }
            set
            {
                _chosenValue = value;
                OnPropertyChanged("ChosenValue");
            }
        }

        public ICommand AddCommand { get; }

        public RecipeCreateViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            RecipeToCreate = new Recipe()
            { 
                Type = RecipeType.Other
            };
            AddCommand = new Command(OnAddCommand);
        }

        private async void OnAddCommand()
        {
            try
            {
                SetRecipeType();
                if (!string.IsNullOrEmpty(RecipeToCreate.Name) && !string.IsNullOrEmpty(RecipeToCreate.Ingredients)
                    && !string.IsNullOrEmpty(RecipeToCreate.Description))
                {
                    CookBookDatabase database = await CookBookDatabase.Instance;
                    await database.SaveItemAsync(RecipeToCreate);
                    _navigationService.GoBack();
                }
            }
            catch
            {
                
            }
        }

        private void SetRecipeType()
        {
            switch (ChosenValue)
            {
                case "Breakfast":
                    RecipeToCreate.Type = RecipeType.Breakfast;
                    break;
                case "Starter":
                    RecipeToCreate.Type = RecipeType.Starter;
                    break;
                case "Soup":
                    RecipeToCreate.Type = RecipeType.Soup;
                    break;
                case "Main Course":
                    RecipeToCreate.Type = RecipeType.MainCourse;
                    break;
                case "Dessert":
                    RecipeToCreate.Type = RecipeType.Dessert;
                    break;
                case "Other":
                    RecipeToCreate.Type = RecipeType.Other;
                    break;
                default:
                    RecipeToCreate.Type = RecipeType.Other;
                    break;
            }
        }
    }
}
