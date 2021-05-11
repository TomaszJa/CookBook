using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace CookBook.Utility
{
    // Klasa umożliwiająca dodanie argumentów do strony
    public static class NavigationExtensions
    {
        private static ConditionalWeakTable<Page, object> arguments = new ConditionalWeakTable<Page, object>();

        public static object GetNavigationArgs(this Page page)
        {
            object argument;
            arguments.TryGetValue(page, out argument);

            return argument;
        }

        // Metoda dodająca argumenty do nawigacji
        public static void SetNavigationArgs(this Page page, object args)
            => arguments.Add(page, args);
    }
}
