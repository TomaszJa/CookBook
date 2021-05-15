using CookBook.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CookBook.Services.Interfaces
{
    // Interfejs, który będzie odpowiedzialny za nawigację.
    // Istnieje po to, żeby nie umieszczać
    // tego kodu w View Modelu, ani View
    public interface INavigationService
    {
        Page MainPage { get; }

        void Configure(string key, Type pageType);
        void GoBack();
        void NavigateTo(string pageKey, object parameter = null);
        void NavigateTo(string pageKey, List<Recipe> recipies, object parameter = null);
        void NavigateTo(string pageKey, List<Recipe> recipies, RecipeType type, object parameter = null);
        void NavigateTo(string pageKey, List<ShoppingListItem> shoppingListItems, object parameter = null);
    }
}
