using CookBook.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookBook.ViewModels.Recipies
{
    public class RecipeIngredientsViewModel : BaseViewModel
    {
        private Recipe _recipe;
        public Recipe RecipeToView
        {
            get { return _recipe; }
            set
            {
                _recipe = value;
                OnPropertyChanged("SelectedErrand");
            }
        }

        public RecipeIngredientsViewModel()
        {
            RecipeToView = new Recipe();
        }

        public override void Initialize(object parameter)
        {
            RecipeToView = parameter as Recipe;
        }
    }
}
