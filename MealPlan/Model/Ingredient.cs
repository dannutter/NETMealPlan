using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MealPlan.Model
{
    public class Ingredient
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        //public Blob Image1 { get; set; }
        //public Blob Image2 { get; set; }
        //public Blob Image3 { get; set; }
    }
}
