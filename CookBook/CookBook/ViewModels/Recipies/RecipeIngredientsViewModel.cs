using CookBook.Data;
using CookBook.Models;
using CookBook.Services.Interfaces;
using CookBook.Utility;
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
                    var index1 = shoppingListItem.Name.IndexOf(' ');
                    var substring1 = shoppingListItem.Name.Substring(index1+1);

                    foreach (var item in itemsFromDatabase)
                    {
                        var ingredientType = item.Name.Split(' ');
                        if (ingredientType.Length > 1)
                        {
                            var index2 = item.Name.IndexOf(' ');
                            var substring2 = item.Name.Substring(index2+1);


                            if (substring1.ToLower() == substring2.ToLower())
                            {
                                item.Name = AddAmount(item.Name);
                                AddAmount(ingredientToAdd, item, ingredientType, "ml", substring2);
                                AddAmount(ingredientToAdd, item, ingredientType, "L", substring2);
                                AddAmount(ingredientToAdd, item, ingredientType, "g", substring2);
                                AddAmount(ingredientToAdd, item, ingredientType, "kg", substring2);
                                var success = AddAmount(ingredientToAdd, item, ingredientType, substring2);

                                added = false;

                                if (shoppingListItem.Name == item.Name && !success)
                                {
                                    added = true;
                                }
                                if (!added)
                                {
                                    await database.SaveShoppingListItemAsync(item);
                                }
                            }
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

        private string AddAmount(string name)
        {
            if (name.Contains("x"))
            {
                var index1 = name.IndexOf('x');

                var value1 = name.Substring(0, index1);
                var value2 = name.Substring(index1 + 1);

                double number1;

                var success1 = double.TryParse(value1, out number1);

                if (success1)
                {
                    name = $"{number1+1}x{value2}";
                    return name;
                }
            }
            else
            {
                name = "2x" + name;
            }
            return name;
        }

        private bool AddAmount(string[] ingredientToAdd, ShoppingListItem item, string[] ingredientType, string fullName)
        {
            double number1, number2;

            var success1 = double.TryParse(ingredientType[0], out number1);
            var success2 = double.TryParse(ingredientToAdd[0], out number2);

            if (success1 && success2)
            {
                number1 += number2;

                item.Name = $"{number1} {fullName}";

                return true;
            }
            return false;
        }

        private static void AddAmount(string[] ingredientToAdd, ShoppingListItem item, string[] ingredientType, string cathegory, string fullName)
        {
            ingredientToAdd = ChangeValue.ChangeToGrams(ingredientToAdd, item, ingredientType, fullName);
            ingredientToAdd = ChangeValue.ChangeToMililiters(ingredientToAdd, item, ingredientType, fullName);

            if (ingredientType[0].Contains(cathegory) && ingredientToAdd[0].Contains(cathegory))
            {
                var index1 = ingredientType[0].IndexOf(cathegory);
                var index2 = ingredientToAdd[0].IndexOf(cathegory);

                var value1 = ingredientType[0].Substring(0, index1);
                var value2 = ingredientToAdd[0].Substring(0, index2);

                double number1, number2;

                var success1 = double.TryParse(value1, out number1);
                var success2 = double.TryParse(value2, out number2);

                if (success1 && success2)
                {
                    number1 += number2;

                    item.Name = $"{number1}{cathegory} {fullName}";
                }
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
