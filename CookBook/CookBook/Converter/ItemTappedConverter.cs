using System;
using System.Globalization;
using Xamarin.Forms;

namespace CookBook.Converter
{
    // Klasa, która pozwala na konwersję otrzymanej wartości na obiekt przekazany przez
    // event args. Dzięki niej działa wybieranie elementów z listy
    class ItemTappedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ItemTappedEventArgs eventArgs)
            {
                return eventArgs.Item;
            }
            return null;
        }

        // Metoda musi zostać zaimplementowana, ponieważ wymaga tego interfejs, ale nie używam jej
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
