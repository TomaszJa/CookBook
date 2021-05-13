using CookBook.Models;
using CookBook.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookBook.ViewModels.Recipies
{
    public class RecipiesTabbedViewModel : BaseViewModel
    {
        private Recipe _recipe;
        public Recipe SelectedRecipe
        {
            get { return _recipe; }
            set
            {
                _recipe = value;
                OnPropertyChanged("SelectedRecipe");
            }
        }
        public RecipeIngredientsViewModel RecipeIngredientsTab { get; set; } = ViewModelLocator.RecipeIngredientsViewModel;
        public RecipeDetailsViewModel RecipeDetailsTab { get; set; } = ViewModelLocator.RecipeDetailsViewModel;

        public RecipiesTabbedViewModel()
        {

        }

        public override void Initialize(object parameter)
        {
            SelectedRecipe = parameter as Recipe;
        }
    }
}
