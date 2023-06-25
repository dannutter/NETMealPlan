using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MealPlan.DataAccess;
using MealPlan.Model;
using Microsoft.Maui.Controls;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Globalization;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace MealPlan.ViewModel;
[QueryProperty(nameof(Id), nameof(Id))]
public partial class MealPlanIndividualViewModel : ObservableObject
{

    [ObservableProperty]
    string id;
    [ObservableProperty]
    string endDate;
    [ObservableProperty]
    Meal_Plan plan;
    List<Recipe> Recipes;
    [ObservableProperty]
    ObservableCollection<Recipe> recipesFiltered;
    [ObservableProperty]
    ObservableCollection<Recipe> recipeDisplay = new();
    public string[] Ingredientliststring;
    public string[] MealType;
    public string[] MealDay;
    public MealPlanIndividualViewModel()
    {

    }
    
    [RelayCommand]
    async void GetData()
    {
        RecipesFiltered = new();
        MealPlanDb database = await MealPlanDb.Instance;
        if (Id != null)
        {

            Plan = await database.GetAsync(int.Parse(Id));
            if (Plan != null)
            {
                EndDate = Plan.StartDate + " --> " + (DateTime.ParseExact(Plan.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(Plan.Duration-1).ToString())[..10];
                CreateRecipeDb recipedb = await CreateRecipeDb.Instance;
                Recipes = await recipedb.GetRecipesAsync();
                string[] RecipeIds = Plan.Meallist.Split(',')
                                           .Select(id => id)
                                           .ToArray();

                List<int> indexes = RecipeIds
                    .Select((value, index) => new { Value = value, Index = index }) // Map each value to its index
                    .Where(item => item.Value != "-") // Filter the items that are not equal to "-"
                    .Select(item => item.Index) // Select only the indexes
                    .ToList();
                List<string> RecipeIdList = RecipeIds.ToList();
                
                RecipesFiltered = new ObservableCollection<Recipe>(RecipeIdList.SelectMany(id => Recipes.Where(recipe => recipe.Name.ToString() == id)));
                if (Recipes != null)
                {
                    Ingredientliststring = new string[RecipeIdList.Count];
                    MealDay = new string[RecipeIdList.Count];
                    MealType = new string[RecipeIdList.Count];
                    for (int index = 0; index < RecipesFiltered.Count; index++)
                    {

                        Ingredientliststring[index] = string.Empty;
                        Recipe Recipe = RecipesFiltered[index];
                        IngredientDb ingredientdb = await IngredientDb.Instance;
                        List<Ingredient> ingredients = await ingredientdb.GetIngAsync();
                        int[] IngredientIds = Recipe.Ingredients.Split(',')
                                           .Select(id => int.Parse(id))
                                           .ToArray();
                        int[] Amounts = Recipe.Amounts.Split(',')
                                           .Select(id => int.Parse(id))
                                           .ToArray();
                        List<Ingredient> IngredientsFiltered = new List<Ingredient>(ingredients.Where(ing => IngredientIds.Contains(ing.Id)));
                        for (int index2 = 0; index2 < IngredientsFiltered.Count; index2++)
                        {
                            Ingredientliststring[index] += IngredientsFiltered[index2].Name + " : " + Amounts[index2] + "g" + "\n";
                        }

                    }
                    for (int index = 0; index < indexes.Count; index++)
                    {
                        int divisor = indexes[index] / 3;
                        MealDay[index] = ((indexes[index] % 7) + 1).ToString();
                        if (indexes[index] >= 0 && indexes[index] <= 6)
                        {
                            MealType[index] = "Breakfast";
                        }
                        else if (indexes[index] >= 7 && indexes[index] <= 13)
                        {
                            MealType[index] = "Lunch";
                        }
                        else if (indexes[index] >= 14 && indexes[index] <= 20)
                        {
                            MealType[index] = "Dinner";
                        }

                    }

                    int[] newindexes = Enumerable.Range(0, RecipesFiltered.Count)
                          .OrderBy(i => MealDay[i])
                          .ToArray();
                    RecipeDisplay = new();
                    for (int index = 0; index < RecipesFiltered.Count; index++)
                    {
                        int sortedIndex = newindexes[index];

                        RecipeDisplay.Add(new Recipe
                        {
                            Name = RecipesFiltered[sortedIndex].Name + "  " + MealType[sortedIndex] + " Day " + MealDay[sortedIndex],
                            Ingredients = Ingredientliststring[sortedIndex],
                            CookTime = RecipesFiltered[sortedIndex].CookTime,
                            PrepTime = RecipesFiltered[sortedIndex].PrepTime,
                            Servings = RecipesFiltered[sortedIndex].Servings,
                            RecipeId = RecipesFiltered[sortedIndex].RecipeId,
                            Instructions = RecipesFiltered[sortedIndex].Instructions,
                            Image = RecipesFiltered[sortedIndex].Image
                        });
                    }
                }

            }
        }

    }

}