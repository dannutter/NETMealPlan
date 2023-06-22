using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPlan.Model
{
    public class Meal_Plan

    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Meallist { get; set; }
        public string StartDate { get; set; }
        public int Duration { get; set; }
        public int Fruitandvegetables { get; set; }
        public int StarchyFood { get; set; }
        public int Dairy { get; set; }
        public int Protein { get; set; }

        public int FatSugar { get; set; }
        public int Other { get; set; }
    }
}
