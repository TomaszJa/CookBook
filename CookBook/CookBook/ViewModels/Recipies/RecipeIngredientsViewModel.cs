using CookBook.Data;
using CookBook.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

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
                OnPropertyChanged("SelectedRecipe");
            }
        }

        private List<string> _ingredients;
        public List<string> Ingredients
        {
            get { return _ingredients; }
            set
            {
                _ingredients = value;
                OnPropertyChanged("SelectedIngredient");
            }
        }

        public ICommand AddToShoppingListCommand { get; }

        public RecipeIngredientsViewModel()
        {
            RecipeToView = new Recipe();
            Ingredients = new List<string>();

            AddToShoppingListCommand = new Command(OnAddToShoppingListCommand);
        }

        private async void OnAddToShoppingListCommand()
        {
            CookBookDatabase database = await CookBookDatabase.Instance;
            foreach (var ingredient in Ingredients)
            {
                var shoppingListItem = new ShoppingListItem
                {
                    Name = ingredient,
                };
                await database.SaveShoppingListItemAsync(shoppingListItem);
            }
        }

        public override void Initialize(object parameter)
        {
            RecipeToView = parameter as Recipe;
            Ingredients.Clear();

            if (!string.IsNullOrEmpty(RecipeToView.Ingredients))
            {
                var listOfIngredients = RecipeToView.Ingredients.Split('\n');

                foreach (var ingredient in listOfIngredients)
                {
                    Ingredients.Add(ingredient);
                }
            }
        }
    }
}
