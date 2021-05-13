using System.Collections.Generic;
using System.Threading.Tasks;
using CookBook.Models;
using SQLite;


namespace CookBook.Data
{
    // Database access class - w niej zawarta jest logika bazodanowa
    public class CookBookDatabase
    {
        static SQLiteAsyncConnection Database;

        // Instance służy do wygenerowania tabeli dla obiektów Recipe.
        public static readonly AsyncLazy<CookBookDatabase> Instance = new AsyncLazy<CookBookDatabase>(async () =>
        {
            var instance = new CookBookDatabase();
            CreateTableResult result = await Database.CreateTableAsync<Recipe>();
            CreateTableResult result1 = await Database.CreateTableAsync<ShoppingListItem>();
            return instance;
        });

        public CookBookDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public Task<Recipe> GetRecipeByIdAsync(int id)
        {
            return Database.Table<Recipe>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<List<Recipe>> GetRecipiesByTypeAsync(RecipeType type)
        {
            return Database.Table<Recipe>().Where(t => t.Type == type).ToListAsync();
        }


        public Task<int> SaveRecipeAsync(Recipe item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteRecipeAsync(Recipe item)
        {
            return Database.DeleteAsync(item);
        }

        // Shopping List Item

        public Task<ShoppingListItem> GetShoppingListItemByIdAsync(int id)
        {
            return Database.Table<ShoppingListItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<List<ShoppingListItem>> GetShoppingListItemsAsync()
        {
            return Database.Table<ShoppingListItem>().ToListAsync();
        }

        public Task<int> SaveShoppingListItemAsync(ShoppingListItem item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteShoppingListItemAsync(ShoppingListItem item)
        {
            return Database.DeleteAsync(item);
        }

        public Task<int> ClearShoppingListAsync()
        {
            return Database.DeleteAllAsync<ShoppingListItem>();
        }
    }
}
