using CookBook.Utility;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookBook.Views.Recipies
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipiesCathegoryView : ContentPage
    {
        public RecipiesCathegoryView()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.RecipiesCathegoryViewModel;
        }
    }
}