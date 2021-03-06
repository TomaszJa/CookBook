using CookBook.Utility;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookBook.Views.Recipies
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipiesTabbedView : TabbedPage
    {
        public RecipiesTabbedView()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.RecipiesTabbedViewModel;
        }
    }
}