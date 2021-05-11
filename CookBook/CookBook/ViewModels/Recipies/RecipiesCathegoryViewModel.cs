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
            var recipies = await database.GetItemsByTypeAsync(Models.RecipeType.Breakfast);

            _navigationService.NavigateTo(ViewNames.RecipiesListView, recipies, "Breakfasts");
        }

        private void OnStartersCommand()
        {
            _navigationService.NavigateTo(ViewNames.RecipiesListView, "Starters");
        }

        private void OnSoupsCommand()
        {
            _navigationService.NavigateTo(ViewNames.RecipiesListView, "Soups");
        }

        private void OnMainCoursesCommand()
        {
            _navigationService.NavigateTo(ViewNames.RecipiesListView, "MainCoursers");
        }

        private void OnDessertsCommand()
        {
            _navigationService.NavigateTo(ViewNames.RecipiesListView, "Desserts");
        }

        private void OnOthersCommand()
        {
            _navigationService.NavigateTo(ViewNames.RecipiesListView, "Others");
        }

        public override void Initialize(object parameter)
        {

        }
    }
}
