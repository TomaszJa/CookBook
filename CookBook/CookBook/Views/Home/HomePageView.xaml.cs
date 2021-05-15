using CookBook.Utility;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookBook.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageView : ContentPage
    {
        public HomePageView()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.HomePageViewModel;
        }
    }
}