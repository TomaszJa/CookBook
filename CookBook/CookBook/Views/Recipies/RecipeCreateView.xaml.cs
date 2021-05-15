using CookBook.Utility;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookBook.Views.Recipies
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeCreateView : ContentPage
    {
        public RecipeCreateView()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.RecipeCreateViewModel;
        }
    }
}