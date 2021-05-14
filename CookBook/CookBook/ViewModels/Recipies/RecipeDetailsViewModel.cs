using CookBook.Data;
using CookBook.Models;
using CookBook.Services.Interfaces;
using CookBook.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CookBook.ViewModels.Recipies
{
    public class RecipeDetailsViewModel : BaseViewModel
    {
        private Recipe _recipe;
        private INavigationService _navigationService;
        private IDialogService _dialogService;

        public Recipe RecipeToView
        {
            get { return _recipe; }
            set
            {
                _recipe = value;
                OnPropertyChanged("RecipeToView");
            }
        }

        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand FollowUrlCommand { get; }

        public RecipeDetailsViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            RecipeToView = new Recipe();

            EditCommand = new Command(OnEditCommand);
            DeleteCommand = new Command(OnDeleteCommand);
            FollowUrlCommand = new Command(OnFollowUrlCommand);

            MessagingCenter.Subscribe<RecipeEditViewModel, Recipe>
                (this, MessageNames.RecipeChangedMessage, OnRecipeChanged);
        }

        private async void OnFollowUrlCommand()
        {
            if (RecipeToView.URL == null)
            {
                await _dialogService.ShowDialog("You didn't specify any Url to this recipe!", "Ups...", "Ok");
            }
            else
            {
                await Launcher.OpenAsync(RecipeToView.URL);
            }
        }

        private void OnRecipeChanged(RecipeEditViewModel sender, Recipe recipe)
        {
            RecipeToView = recipe;
        }

        private async void OnDeleteCommand()
        {
            var result = await _dialogService.ConfirmDialog("Are you sure you want to DELETE this recipe?", "Delete Recipe", "Yes", "No");

            if (result)
            {
                CookBookDatabase database = await CookBookDatabase.Instance;
                await database.DeleteRecipeAsync(RecipeToView);
                MessagingCenter.Send(this, MessageNames.RecipeChangedMessage, RecipeToView);

                _navigationService.GoBack();
            }
        }

        private void OnEditCommand()
        {
            ViewModelLocator.RecipeEditViewModel.Initialize(RecipeToView);
            _navigationService.NavigateTo(ViewNames.RecipeEditView, RecipeToView);
        }

        public override void Initialize(object parameter)
        {
            RecipeToView = parameter as Recipe;
        }
    }
}
