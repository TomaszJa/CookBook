using System;
using System.Collections.Generic;
using System.Text;
using CookBook.ViewModels;
using CookBook.ViewModels.Home;

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
            = new HomePageViewModel();
    }
}
