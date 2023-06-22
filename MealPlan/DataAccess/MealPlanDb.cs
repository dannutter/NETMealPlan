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
    public class MealPlanDb
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<MealPlanDb> Instance =
            new AsyncLazy<MealPlanDb>(async () =>
            {
                var Db = new MealPlanDb();
                try
                {
                    CreateTableResult result = await Database.CreateTableAsync<Meal_Plan>();
                }
                catch
                {

                    throw;
                }
                return Db;
            });


        public MealPlanDb()
        {
            Database = new SQLiteAsyncConnection(SQLdb.DatabasePath, SQLdb.Flags);
        }

        public Task<List<Meal_Plan>> GetRecipesAsync()
        {
            return Database.Table<Meal_Plan>().ToListAsync();
        }

        public Task<Meal_Plan> GetAsync(int id)
        {
            return Database.Table<Meal_Plan>().Where(mealplan => mealplan.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveAsync(Meal_Plan mealplan)
        {
            if (mealplan.Id != 0)
            {
                return Database.UpdateAsync(mealplan);
            }
            else
            {
                return Database.InsertAsync(mealplan);
            }
        }

        public Task<int> DeleteAsync(Meal_Plan mealplan)
        {
            return Database.DeleteAsync(mealplan);
        }
    }
}
