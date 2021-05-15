using CookBook.Utility;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookBook.Views.Recipies
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeEditView : ContentPage
    {
        public RecipeEditView()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.RecipeEditViewModel;
        }
    }
}