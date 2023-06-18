using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MealPlan.Model;
using MealPlan.Services;
using System.Data.Entity;
//inspiration from MAUI documentation
namespace MealPlan.DataAccess
{
    public class IngredientDb
    {
        static SQLiteAsyncConnection IngDatabase;

        public static readonly AsyncLazy<IngredientDb> Instance =
            new AsyncLazy<IngredientDb>(async () =>
            {
                var IngDb = new IngredientDb();
                try
                {
                    CreateTableResult result = await IngDatabase.CreateTableAsync<Ingredient>();
                }
                catch
                {

                    throw;
                }
                return IngDb;
            });


        public IngredientDb()
        {
            IngDatabase = new SQLiteAsyncConnection(SQLdb.DatabasePath, SQLdb.Flags);
        }

        public Task<List<Ingredient>> GetRecipesAsync()
        {
            return IngDatabase.Table<Ingredient>().ToListAsync();
        }

        public Task<Ingredient> GetAsync(string name)
        {
            return IngDatabase.Table<Ingredient>().Where(ingredient => ingredient.Name == name).FirstOrDefaultAsync();
        }

        public Task<int> SaveAsync(Ingredient ingredient)
        {
            if (ingredient.Id != 0)
            {
                return IngDatabase.UpdateAsync(ingredient);
            }
            else
            {
                return IngDatabase.InsertAsync(ingredient);
            }
        }

        public Task<int> DeleteAsync(Ingredient ingredient)
        {
            return IngDatabase.DeleteAsync(ingredient);
        }
    }
}
