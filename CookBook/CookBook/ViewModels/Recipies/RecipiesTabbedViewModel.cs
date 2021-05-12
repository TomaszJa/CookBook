using CookBook.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookBook.ViewModels.Recipies
{
    public class RecipiesTabbedViewModel : BaseViewModel
    {
        public RecipeIngredientsViewModel RecipeIngredientsTab { get; set; } = ViewModelLocator.RecipeIngredientsViewModel;
        public RecipeDetailsViewModel RecipeDetailsTab { get; set; } = ViewModelLocator.RecipeDetailsViewModel;

        public override void Initialize(object parameter)
        {
            RecipeIngredientsTab.Initialize(parameter);
            RecipeDetailsTab.Initialize(parameter);
        }
    }
}
