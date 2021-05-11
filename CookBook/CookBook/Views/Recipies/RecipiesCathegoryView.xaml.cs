using CookBook.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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