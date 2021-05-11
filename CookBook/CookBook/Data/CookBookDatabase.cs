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
            return instance;
        });

        public CookBookDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public Task<Recipe> GetItemByIdAsync(int id)
        {
            return Database.Table<Recipe>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<List<Recipe>> GetItemsByTypeAsync(RecipeType type)
        {
            return Database.Table<Recipe>().Where(t => t.Type == type).ToListAsync();
        }

        public Task<int> SaveItemAsync(Recipe item)
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

        public Task<int> DeleteItemAsync(Recipe item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
