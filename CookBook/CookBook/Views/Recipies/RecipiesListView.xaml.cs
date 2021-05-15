using CookBook.Utility;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookBook.Views.Recipies
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipiesListView : ContentPage
    {
        public RecipiesListView()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.RecipiesListViewModel;
        }
    }
}