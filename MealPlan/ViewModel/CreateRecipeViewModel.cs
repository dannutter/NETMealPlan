using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPlan.ViewModel
{
    public partial class CreateRecipeViewModel : ObservableObject
    {

       
        [ObservableProperty]
        ObservableCollection<string> ingredients;

        public CreateRecipeViewModel()
        {
            Ingredients = new ObservableCollection<string>();
        }
        [ObservableProperty]
        string recipeName, cook, prep, servings,instructions;

        [ObservableProperty]
        List<string> groups = new List<string>()
        {
            "Fruit and vegetables",
            "Starchy food",
            "Dairy",
            "Protein",
            "High in Fat/Sugar",
            "Other"
        };

        [ObservableProperty]
        string ingredientName, grams, group;


        [RelayCommand]
        void SubIngredient()
        {
            if (string.IsNullOrWhiteSpace(IngredientName) || string.IsNullOrWhiteSpace(Grams) || string.IsNullOrWhiteSpace(Group))
            {
                return;
            }
            string Temp = IngredientName + "    |   " + Grams + "g   |  " + Group;
            Ingredients.Add(Temp);
            IngredientName = Grams = Group = string.Empty;
        }
        [RelayCommand]
        void Delete(string r)
        { 
            if (Ingredients.Contains(r))
            {
                Ingredients.Remove(r);
            }
        }
    }
}
