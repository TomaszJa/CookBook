﻿using CookBook.Data;
using CookBook.Models;
using CookBook.Services.Interfaces;
using CookBook.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CookBook.ViewModels.Home
{
    public class HomePageViewModel : BaseViewModel
    {
        private INavigationService _navigationService;

        public ICommand AddRecipyCommand { get; }
        public ICommand BrowseRecipiesCommand { get; }

        public HomePageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            AddRecipyCommand = new Command(OnAddRecipyCommand);
            BrowseRecipiesCommand = new Command(OnBrowseRecipiesCommand);
        }

        private void OnBrowseRecipiesCommand()
        {
            _navigationService.NavigateTo(ViewNames.RecipiesCathegoryView);
        }

        private async void OnAddRecipyCommand()
        {
            var recipe = new Recipe
            {
                Name = "Kanapka",
                Type = RecipeType.Breakfast
            };

            CookBookDatabase database = await CookBookDatabase.Instance;
            await database.SaveItemAsync(recipe);
        }
    }
}
