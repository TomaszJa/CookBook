using CookBook.Data;
using CookBook.Models;
using CookBook.Services.Interfaces;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace CookBook.ViewModels.Recipies
{
    public class RecipeIngredientsViewModel : BaseViewModel
    {
        private Recipe _recipe;
        private IDialogService _dialogService;

        public Recipe RecipeToView
        {
            get { return _recipe; }
            set
            {
                _recipe = value;
                OnPropertyChanged("RecipeToView");
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

        public RecipeIngredientsViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

            RecipeToView = new Recipe();
            Ingredients = new List<string>();

            AddToShoppingListCommand = new Command(OnAddToShoppingListCommand);
        }

        private async void OnAddToShoppingListCommand()
        {
            CookBookDatabase database = await CookBookDatabase.Instance;
            var itemsFromDatabase = await database.GetShoppingListItemsAsync();
            var added = true;

            foreach (var ingredient in Ingredients)
            {
                added = true;
                var shoppingListItem = new ShoppingListItem
                {
                    Name = ingredient
                };

                var ingredientToAdd = ingredient.Split(' ');

                if (ingredientToAdd.Length > 1)
                {
                    foreach (var item in itemsFromDatabase)
                    {
                        var ingredientType = item.Name.Split(' ');
                        if (ingredientType.Length > 1 && ingredientType[1] == ingredientToAdd[1])
                        {
                            item.Name = $"{ingredientToAdd[0]}+{item.Name}";
                            await database.SaveShoppingListItemAsync(item);

                            added = false;
                        }
                    }
                }

                if (added)
                {
                    await database.SaveShoppingListItemAsync(shoppingListItem);
                }
            }
            await _dialogService.ShowDialog("Ingredients added to shopping list!", "Success", "Ok");
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
