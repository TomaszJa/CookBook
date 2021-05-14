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
    public class RecipiesCathegoryViewModel : BaseViewModel
    {
        private INavigationService _navigationService;

        public ICommand BreakfastsCommand { get; }
        public ICommand StartersCommand { get; }
        public ICommand SoupsCommand { get; }
        public ICommand MainCoursesCommand { get; }
        public ICommand DessertsCommand { get; }
        public ICommand OthersCommand { get; }

        public RecipiesCathegoryViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            BreakfastsCommand = new Command(OnBreakfastsCommand);
            StartersCommand = new Command(OnStartersCommand);
            SoupsCommand = new Command(OnSoupsCommand);
            MainCoursesCommand = new Command(OnMainCoursesCommand);
            DessertsCommand = new Command(OnDessertsCommand);
            OthersCommand = new Command(OnOthersCommand);
        }

        private async void OnBreakfastsCommand()
        {
            CookBookDatabase database = await CookBookDatabase.Instance;
            var recipies = await database.GetRecipiesByTypeAsync(RecipeType.Breakfast);

            _navigationService.NavigateTo(ViewNames.RecipiesListView, recipies, RecipeType.Breakfast, "Breakfasts");
        }

        private async void OnStartersCommand()
        {
            CookBookDatabase database = await CookBookDatabase.Instance;
            var recipies = await database.GetRecipiesByTypeAsync(RecipeType.Starter);

            _navigationService.NavigateTo(ViewNames.RecipiesListView, recipies, RecipeType.Starter, "Starters");
        }

        private async void OnSoupsCommand()
        {
            CookBookDatabase database = await CookBookDatabase.Instance;
            var recipies = await database.GetRecipiesByTypeAsync(RecipeType.Soup);

            _navigationService.NavigateTo(ViewNames.RecipiesListView, recipies, RecipeType.Soup, "Soups");
        }

        private async void OnMainCoursesCommand()
        {
            CookBookDatabase database = await CookBookDatabase.Instance;
            var recipies = await database.GetRecipiesByTypeAsync(RecipeType.MainCourse);

            _navigationService.NavigateTo(ViewNames.RecipiesListView, recipies, RecipeType.MainCourse, "MainCoursers");
        }

        private async void OnDessertsCommand()
        {
            CookBookDatabase database = await CookBookDatabase.Instance;
            var recipies = await database.GetRecipiesByTypeAsync(RecipeType.Dessert);

            _navigationService.NavigateTo(ViewNames.RecipiesListView, recipies, RecipeType.Dessert, "Desserts");
        }

        private async void OnOthersCommand()
        {
            CookBookDatabase database = await CookBookDatabase.Instance;
            var recipies = await database.GetRecipiesByTypeAsync(RecipeType.Other);

            _navigationService.NavigateTo(ViewNames.RecipiesListView, recipies, RecipeType.Other, "Others");
        }

        public override void Initialize(object parameter)
        {

        }
    }
}
