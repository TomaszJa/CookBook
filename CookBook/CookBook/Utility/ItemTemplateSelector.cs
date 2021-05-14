using CookBook.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CookBook.Utility
{
    // Klasa potrzebna do wyboru Templatki obiektu, w tym przypadku listy zakupów - czerwony/zielony
    public class ItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NotBoughtLabel { get; set; }
        public DataTemplate BoughtLabel { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((ShoppingListItem)item).Check ? BoughtLabel : NotBoughtLabel;
        }
    }
}
