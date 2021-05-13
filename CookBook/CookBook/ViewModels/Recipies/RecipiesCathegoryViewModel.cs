using CookBook.Data;
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
            var recipies = await database.GetRecipiesByTypeAsync(Models.RecipeType.Breakfast);

            _navigationService.NavigateTo(ViewNames.RecipiesListView, recipies, "Breakfasts");
        }

        private async void OnStartersCommand()
        {
            CookBookDatabase database = await CookBookDatabase.Instance;
            var recipies = await database.GetRecipiesByTypeAsync(Models.RecipeType.Starter);

            _navigationService.NavigateTo(ViewNames.RecipiesListView, recipies, "Starters");
        }

        private async void OnSoupsCommand()
        {
            CookBookDatabase database = await CookBookDatabase.Instance;
            var recipies = await database.GetRecipiesByTypeAsync(Models.RecipeType.Soup);

            _navigationService.NavigateTo(ViewNames.RecipiesListView, recipies, "Soups");
        }

        private async void OnMainCoursesCommand()
        {
            CookBookDatabase database = await CookBookDatabase.Instance;
            var recipies = await database.GetRecipiesByTypeAsync(Models.RecipeType.MainCourse);

            _navigationService.NavigateTo(ViewNames.RecipiesListView, recipies, "MainCoursers");
        }

        private async void OnDessertsCommand()
        {
            CookBookDatabase database = await CookBookDatabase.Instance;
            var recipies = await database.GetRecipiesByTypeAsync(Models.RecipeType.Dessert);

            _navigationService.NavigateTo(ViewNames.RecipiesListView, recipies, "Desserts");
        }

        private async void OnOthersCommand()
        {
            CookBookDatabase database = await CookBookDatabase.Instance;
            var recipies = await database.GetRecipiesByTypeAsync(Models.RecipeType.Other);

            _navigationService.NavigateTo(ViewNames.RecipiesListView, recipies, "Others");
        }

        public override void Initialize(object parameter)
        {

        }
    }
}
