using CookBook.Utility;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookBook.Views.Recipies
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeDetailsView : ContentPage
    {
        public RecipeDetailsView()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.RecipeDetailsViewModel;
        }
    }
}