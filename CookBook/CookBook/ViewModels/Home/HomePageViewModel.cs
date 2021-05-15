using CookBook.Data;
using CookBook.Models;
using CookBook.Services.Interfaces;
using CookBook.Utility;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace CookBook.ViewModels.Home
{
    public class HomePageViewModel : BaseViewModel
    {
        private INavigationService _navigationService;

        public ICommand AddRecipyCommand { get; }
        public ICommand BrowseRecipiesCommand { get; }
        public ICommand ShoppingListCommand { get; }

        public HomePageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            AddRecipyCommand = new Command(OnAddRecipyCommand);
            BrowseRecipiesCommand = new Command(OnBrowseRecipiesCommand);
            ShoppingListCommand = new Command(OnShoppingListCommand);
        }

        private async void OnShoppingListCommand()
        {
            CookBookDatabase database = await CookBookDatabase.Instance;
            List<ShoppingListItem> items = await database.GetShoppingListItemsAsync();

            _navigationService.NavigateTo(ViewNames.ShoppingListView, items, null);
        }

        private void OnBrowseRecipiesCommand()
        {
            _navigationService.NavigateTo(ViewNames.RecipiesCathegoryView);
        }

        private void OnAddRecipyCommand()
        {
            _navigationService.NavigateTo(ViewNames.RecipeCreateView);
        }
    }
}
