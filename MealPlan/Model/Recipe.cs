using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MealPlan.Model
{
    public class Recipe
    {
        [PrimaryKey, AutoIncrement]
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        //public string Amounts { get; set; }
        //public Blob Image { get; set; }
        public int CookTime { get; set; }
        public int PrepTime { get; set; }
        public int Servings { get; set; }
        public int Fruitandvegetables { get; set; }
        public int StarchyFood { get; set; }
        public int Dairy { get; set; }
        public int Protein { get; set; }

        public int FatSugar { get; set; }
        public int Other { get; set; }
        public string Instructions { get; set; }
    }
}