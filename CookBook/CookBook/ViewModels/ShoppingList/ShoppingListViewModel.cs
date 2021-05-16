using CookBook.Data;
using CookBook.Models;
using CookBook.Services.Interfaces;
using CookBook.Utility;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace CookBook.ViewModels.ShoppingList
{
    public class ShoppingListViewModel : BaseViewModel
    {
        private ObservableCollection<ShoppingListItem> _shoppingListItems;
        private IDialogService _dialogService;

        public ObservableCollection<ShoppingListItem> ShoppingListItems
        {
            get => _shoppingListItems;
            set
            {
                _shoppingListItems = value;
                OnPropertyChanged("ShoppingListItem");
            }
        }

        public ICommand ClearShoppingListCommand { get; }
        public ICommand ItemSelectedCommand { get; }
        public ICommand AddItemCommand { get; }

        public ShoppingListViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

            ShoppingListItems = new ObservableCollection<ShoppingListItem>();
            ClearShoppingListCommand = new Command(OnClearShoppingListCommand);

            ItemSelectedCommand = new Command<ShoppingListItem>(OnItemSelectedCommand);
            AddItemCommand = new Command(OnAddItemCommand);
        }

        private async void OnAddItemCommand()
        {
            var input = await _dialogService.InputDialog("Provide Shopping Item", "Add Item", "Confirm", "Cancel");
            CookBookDatabase database = await CookBookDatabase.Instance;
            var added = true;

            if (input.Ok)
            {
                var newItem = new ShoppingListItem()
                {
                    Name = input.Value
                };

                var ingredientToAdd = newItem.Name.Split(' ');

                if (ingredientToAdd.Length > 1)
                {
                    var index1 = newItem.Name.IndexOf(' ');
                    var substring1 = newItem.Name.Substring(index1+1);

                    foreach (var item in ShoppingListItems)
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

                                if (newItem.Name == item.Name && !success)
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
                    await database.SaveShoppingListItemAsync(newItem);
                    ShoppingListItems.Insert(0, newItem);
                }
            }
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

        private async void OnItemSelectedCommand(ShoppingListItem item)
        {
            var options = new[] { "Item Bought" };
            var choiceOptions = await _dialogService.ActionSheet("Choose Action", "Cancel", "Delete", options);
            CookBookDatabase database = await CookBookDatabase.Instance;

            switch (choiceOptions)
            {
                case "Item Bought":
                    item.Check = true;
                    await database.SaveShoppingListItemAsync(item);
                    ShoppingListItems.Remove(item);
                    ShoppingListItems.Add(item);
                    break;
                case "Delete":
                    var result = await _dialogService.ConfirmDialog("Are you sure you want to DELETE this item from the Shopping List?", "Delete Item", "Yes", "No");

                    if (result)
                    {
                        await database.DeleteShoppingListItemAsync(item);
                        ShoppingListItems.Remove(item);
                    }
                    break;
                default:
                    break;
            }
        }

        private async void OnClearShoppingListCommand()
        {
            var result = await _dialogService.ConfirmDialog("Are you sure you want to clear the Shopping List?", "Clear List", "Yes", "No");

            if (result)
            {
                CookBookDatabase database = await CookBookDatabase.Instance;
                await database.ClearShoppingListAsync();
                ShoppingListItems.Clear();
            }
        }

        public override void Initialize(object parameter, List<ShoppingListItem> shoppingListItems)
        {
            ShoppingListItems.Clear();
            foreach (var item in shoppingListItems)
            {
                ShoppingListItems.Add(item);
            }
        }
    }
}
