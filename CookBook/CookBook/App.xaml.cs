using CookBook.Services.Interfaces;
using CookBook.Services.ServiceClasses;
using CookBook.Utility;
using CookBook.Views.Home;
using CookBook.Views.Recipies;
using CookBook.Views.ShoppingList;
using Xamarin.Forms;

namespace CookBook
{
    public partial class App : Application
    {
        public static NavigationService NavigationService { get; } = new NavigationService();
        public static IDialogService DialogService { get; } = new DialogService();

        public App()
        {
            InitializeComponent();

            // Konfiguracja NavigationService - dodajemy do serwisu informacje o istniejących 
            // stronach (Widokach)
            NavigationService.Configure(ViewNames.HomePageView, typeof(HomePageView));
            NavigationService.Configure(ViewNames.RecipiesTabbedView, typeof(RecipiesTabbedView));
            NavigationService.Configure(ViewNames.RecipiesListView, typeof(RecipiesListView));
            NavigationService.Configure(ViewNames.RecipiesCathegoryView, typeof(RecipiesCathegoryView));
            NavigationService.Configure(ViewNames.RecipeCreateView, typeof(RecipeCreateView));
            NavigationService.Configure(ViewNames.RecipeIngredientsView, typeof(RecipeIngredientsView));
            NavigationService.Configure(ViewNames.RecipeDetailsView, typeof(RecipeDetailsView));
            NavigationService.Configure(ViewNames.RecipeEditView, typeof(RecipeEditView));
            NavigationService.Configure(ViewNames.ShoppingListView, typeof(ShoppingListView));


            MainPage = new NavigationPage(new HomePageView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
