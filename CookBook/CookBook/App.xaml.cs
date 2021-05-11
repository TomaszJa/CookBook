using CookBook.Services.ServiceClasses;
using CookBook.Utility;
using CookBook.Views.Home;
using CookBook.Views.Recipies;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookBook
{
    public partial class App : Application
    {
        public static NavigationService NavigationService { get; } = new NavigationService();

        public App()
        {
            InitializeComponent();

            // Konfiguracja NavigationService - dodajemy do serwisu informacje o istniejących 
            // stronach (Widokach)
            NavigationService.Configure(ViewNames.HomePageView, typeof(HomePageView));
            NavigationService.Configure(ViewNames.RecipiesTabbedView, typeof(RecipiesTabbedView));
            NavigationService.Configure(ViewNames.RecipiesListView, typeof(RecipiesListView));
            NavigationService.Configure(ViewNames.RecipiesCathegoryView, typeof(RecipiesCathegoryView));

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
