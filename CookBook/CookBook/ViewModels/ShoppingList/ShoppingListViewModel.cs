using CookBook.Data;
using CookBook.Models;
using CookBook.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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

        public ShoppingListViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

            ShoppingListItems = new ObservableCollection<ShoppingListItem>();
            ClearShoppingListCommand = new Command(OnClearShoppingListCommand);

            ItemSelectedCommand = new Command<ShoppingListItem>(OnItemSelectedCommand);
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
            CookBookDatabase database = await CookBookDatabase.Instance;
            await database.ClearShoppingListAsync();
            ShoppingListItems.Clear();
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
