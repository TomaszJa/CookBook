using System;
using System.Collections.Generic;
using System.Text;
using CookBook.ViewModels;
using CookBook.ViewModels.Home;
using CookBook.ViewModels.Recipies;
using CookBook.ViewModels.ShoppingList;

namespace CookBook.Utility
{
    // Klasa odpowiedzialna za przekazywanie widokom odpowiednich 
    // View Models. Każdy Widok wie o istnieniu ViewModelLocatora,
    // a ViewModelLocator wie o istnieniu każdego widoku
    // ISTOTNA JEST KOLEJNOŚĆ TWORZENIA!
    // Tabbed musi być poniżej swoich "dzieci"
    public static class ViewModelLocator
    {
        public static HomePageViewModel HomePageViewModel { get; set; }
            = new HomePageViewModel(App.NavigationService);

        public static RecipeCreateViewModel RecipeCreateViewModel { get; set; }
            = new RecipeCreateViewModel();
        public static RecipeDetailsViewModel RecipeDetailsViewModel { get; set; }
            = new RecipeDetailsViewModel();
        public static RecipeIngredientsViewModel RecipeIngredientsViewModel { get; set; }
            = new RecipeIngredientsViewModel();
        public static RecipiesListViewModel RecipiesListViewModel { get; set; }
            = new RecipiesListViewModel(App.NavigationService);
        public static RecipiesCathegoryViewModel RecipiesCathegoryViewModel { get; set; }
            = new RecipiesCathegoryViewModel(App.NavigationService);
        public static RecipiesTabbedViewModel RecipiesTabbedViewModel { get; set; }
            = new RecipiesTabbedViewModel();

        public static ShoppingListViewModel ShoppingListViewModel { get; set; }
            = new ShoppingListViewModel();
    }
}
