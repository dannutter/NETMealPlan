using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MealPlan.Model;
//inspiration from MAUI documentation
namespace MealPlan.DataAccess
{
    public class CreateRecipeDb
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<CreateRecipeDb> Instance =
            new AsyncLazy<CreateRecipeDb>(async () =>
            {
                var Db = new CreateRecipeDb();
                try
                {
                    CreateTableResult result = await Database.CreateTableAsync<Recipe>();
                }
                catch
                {

                    throw;
                }
                return Db;
            });


        public CreateRecipeDb()
        {
            Database = new SQLiteAsyncConnection(SQLdb.DatabasePath, SQLdb.Flags);
        }

        public Task<List<Recipe>> GetRecipesAsync()
        {
            return Database.Table<Recipe>().ToListAsync();
        }

        public Task<Recipe> GetAsync(int id)
        {
            return Database.Table<Recipe>().Where(recipe => recipe.RecipeId == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveAsync(Recipe recipe)
        {
            if (recipe.RecipeId != 0)
            {
                return Database.UpdateAsync(recipe);
            }
            else
            {
                return Database.InsertAsync(recipe);
            }
        }

        public Task<int> DeleteAsync(Recipe recipe)
        {
            return Database.DeleteAsync(recipe);
        }
    }
}
