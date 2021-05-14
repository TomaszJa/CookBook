using CookBook.Data;
using CookBook.Models;
using CookBook.Services.Interfaces;
using CookBook.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CookBook.ViewModels.Recipies
{
    public class RecipeEditViewModel : BaseViewModel
    {
        private Recipe _recipe;
        private List<string> _types = new List<string>()
        {
            "Breakfast", "Starter", "Soup", "Main Course", "Dessert", "Other"
        };
        private string _chosenValue;
        private IDialogService _dialogService;
        private INavigationService _navigationService;

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
        public Recipe RecipeToEdit
        {
            get { return _recipe; }
            set
            {
                _recipe = value;
                OnPropertyChanged("SelectedRecipe");
            }
        }

        public ICommand SaveCommand { get; }

        public RecipeEditViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;

            RecipeToEdit = new Recipe();

            SaveCommand = new Command(OnSaveCommand);
        }

        private async void OnSaveCommand()
        {
            var validInput = false;
            try
            {
                SetRecipeType();
                if (!string.IsNullOrEmpty(RecipeToEdit.Name) && !string.IsNullOrEmpty(RecipeToEdit.Ingredients)
                    && !string.IsNullOrEmpty(RecipeToEdit.Description))
                {
                    validInput = true;
                }
                if (!string.IsNullOrEmpty(RecipeToEdit.StringURL))
                {
                    validInput = true;
                    try
                    {
                        RecipeToEdit.URL = new Uri(RecipeToEdit.StringURL);
                    }
                    catch
                    {
                        await _dialogService.ShowDialog("You should specify a correct Url", "Wrong input", "Ok");
                        validInput = false;
                    }
                }
                if (validInput)
                {
                    CookBookDatabase database = await CookBookDatabase.Instance;
                    await database.SaveRecipeAsync(RecipeToEdit);
                    MessagingCenter.Send(this, MessageNames.RecipeChangedMessage, RecipeToEdit);
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
                    RecipeToEdit.Type = RecipeType.Breakfast;
                    break;
                case "Starter":
                    RecipeToEdit.Type = RecipeType.Starter;
                    break;
                case "Soup":
                    RecipeToEdit.Type = RecipeType.Soup;
                    break;
                case "Main Course":
                    RecipeToEdit.Type = RecipeType.MainCourse;
                    break;
                case "Dessert":
                    RecipeToEdit.Type = RecipeType.Dessert;
                    break;
                case "Other":
                    RecipeToEdit.Type = RecipeType.Other;
                    break;
                default:
                    break;
            }
        }

        public override void Initialize(object parameter)
        {
            RecipeToEdit = parameter as Recipe;
        }
    }
}
