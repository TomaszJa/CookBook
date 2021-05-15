using CookBook.Utility;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookBook.Views.ShoppingList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShoppingListView : ContentPage
    {
        public ShoppingListView()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.ShoppingListViewModel;
        }
    }
}