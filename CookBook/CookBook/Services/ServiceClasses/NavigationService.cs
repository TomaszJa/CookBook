using CookBook.Models;
using CookBook.Services.Interfaces;
using CookBook.Utility;
using CookBook.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CookBook.Services.ServiceClasses
{
    public class NavigationService : INavigationService
    {
        // Słownik zawierający wszystkie strony w aplikacji. Kluczem jest nazwa strony
        // podana jako string, wartością natomiast typ strony np.:
        // ("ProjectDetailView", typeof(ProjectDetailView))
        private Dictionary<string, Type> pages { get; } = new Dictionary<string, Type>();

        // Sprawia, że navigation service jest zależne od UI
        public Page MainPage => Application.Current.MainPage;

        // Metoda służaca do konfiguracji słownika, czyli dodawania informacji
        // o isntiejących stronach
        public void Configure(string key, Type pageType) => pages[key] = pageType;

        // Wykorzystuje stronę aplikacji i wywołuje na niej PopAsync(), który przenosi
        // nas do poprzedniej strony w staku
        public void GoBack() => MainPage.Navigation.PopAsync();

        // Przyjmuje jako argument klucz w słowniku strony, do której chcę 
        // się przemieścić, jak również parametr
        public void NavigateTo(string pageKey, object parameter = null)
        {
            if (pages.TryGetValue(pageKey, out Type pageType))
            {
                // Utworzenie wybranej strony
                var page = (Page)Activator.CreateInstance(pageType);
                page.SetNavigationArgs(parameter);

                // I przejście do niej
                MainPage.Navigation.PushAsync(page);

                // Ustawiam BindingContext w stronie, na któą przechodzę
                // na obiekt przekazany z poprzedniej strony
                // Wykorzystuję do tego metodę virtualną Initialize
                (page.BindingContext as BaseViewModel).Initialize(parameter);
            }
            else
            {
                throw new ArgumentException($"This page doesn't exist: {pageKey}.", nameof(pageKey));
            }
        }

        public void NavigateTo(string pageKey, List<Recipe> recipies, object parameter = null)
        {
            if (pages.TryGetValue(pageKey, out Type pageType))
            {
                // Utworzenie wybranej strony
                var page = (Page)Activator.CreateInstance(pageType);
                page.SetNavigationArgs(parameter);

                // I przejście do niej
                MainPage.Navigation.PushAsync(page);

                // Ustawiam BindingContext w stronie, na któą przechodzę
                // na obiekt przekazany z poprzedniej strony
                // Wykorzystuję do tego metodę virtualną Initialize
                (page.BindingContext as BaseViewModel).Initialize(parameter, recipies);
            }
            else
            {
                throw new ArgumentException($"This page doesn't exist: {pageKey}.", nameof(pageKey));
            }
        }

        public void NavigateTo(string pageKey, List<Recipe> recipies, RecipeType type, object parameter = null)
        {
            if (pages.TryGetValue(pageKey, out Type pageType))
            {
                // Utworzenie wybranej strony
                var page = (Page)Activator.CreateInstance(pageType);
                page.SetNavigationArgs(parameter);

                // I przejście do niej
                MainPage.Navigation.PushAsync(page);

                // Ustawiam BindingContext w stronie, na któą przechodzę
                // na obiekt przekazany z poprzedniej strony
                // Wykorzystuję do tego metodę virtualną Initialize
                (page.BindingContext as BaseViewModel).Initialize(parameter, recipies, type);
            }
            else
            {
                throw new ArgumentException($"This page doesn't exist: {pageKey}.", nameof(pageKey));
            }
        }

        public void NavigateTo(string pageKey, List<ShoppingListItem> shoppingListItems, object parameter = null)
        {
            if (pages.TryGetValue(pageKey, out Type pageType))
            {
                // Utworzenie wybranej strony
                var page = (Page)Activator.CreateInstance(pageType);
                page.SetNavigationArgs(parameter);

                // I przejście do niej
                MainPage.Navigation.PushAsync(page);

                // Ustawiam BindingContext w stronie, na któą przechodzę
                // na obiekt przekazany z poprzedniej strony
                // Wykorzystuję do tego metodę virtualną Initialize
                (page.BindingContext as BaseViewModel).Initialize(parameter, shoppingListItems);
            }
            else
            {
                throw new ArgumentException($"This page doesn't exist: {pageKey}.", nameof(pageKey));
            }
        }
    }
}
