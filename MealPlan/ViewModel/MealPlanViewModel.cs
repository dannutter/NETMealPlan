using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MealPlan.DataAccess;
using MealPlan.Model;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq.Expressions;

namespace MealPlan.ViewModel
{
	public partial class MealPlanViewModel : ObservableObject
	{
        public string[] MealList = Enumerable.Repeat("-", 21).ToArray();
        [ObservableProperty]
		ObservableCollection<string> selectedDate = new() ;
		[ObservableProperty]
		string selectSlot;
        [ObservableProperty]
        bool hideTable = true;
        [ObservableProperty]
        bool hideMenu = false;
        [ObservableProperty]
		int duration =1 ;
		[ObservableProperty]
		string startDate;
		[ObservableProperty]
        ObservableCollection<MealSlot> mealSlots = new();
        private ObservableCollection<Recipe> meals;
        public ObservableCollection<Recipe> Meals
        {
            get
            {
                return meals;
            }
            set
            {
                meals = value;
                OnPropertyChanged("Meals");
            }
        }
        public MealPlanViewModel()
		{
            StartDate = ((DateTime.Today).ToString())[..10];
            GetallMeals();
        }
        async void GetallMeals()
        {
            CreateRecipeDb database = await CreateRecipeDb.Instance;
            Meals = new ObservableCollection<Recipe>((await database.GetRecipesAsync()) as List<Recipe>);
        }
        [RelayCommand]
        void CreateTable()
        {
			SelectedDate= new ObservableCollection<string>();
            MealSlots = new ObservableCollection<MealSlot>();	
            DateTime Dated = DateTime.Parse(StartDate);
			
			for (int i = 0; i < 7; i++)
			{
                for (int X = 0; X < 3; X++)
                {
                    if (i < Duration)
					{
                        MealSlots.Add(new MealSlot { Visible = "true",MealId =((i)+(X*7)).ToString() , Text="Select"});
						if (X == 0)
						{
							SelectedDate.Add(((Dated.AddDays(i)).ToString())[..10]);
						}
					}
					else
					{
                        MealSlots.Add(new MealSlot { Visible = "false", MealId = ((i + 1) * (X + 1)).ToString(), Text = "Select" });
                    }
				}

			}
        }
		[RelayCommand]
		void SelectMenu(string slot) 
		{
			SelectSlot = slot;
            HideTable = false;
            HideMenu = true;
        }
        private Recipe selectMeal;
        public Recipe SelectMeal
        {
            get { return selectMeal; }
            set
            {
                if (selectMeal != value)
                {
                    selectMeal = value;
                    OnPropertyChanged("SelectMeal");
                    if (selectMeal != null)
                    {
                        HideTable = true;
                        HideMenu = false;
                        ChosenMeal(SelectMeal.Name);
                    }
                }
            }
        }
        void ChosenMeal (string MealName)
        {
            MealList[int.Parse(SelectSlot)]= MealName;
        }
        [RelayCommand]
        void CancelSelect()
        {
            HideTable = true;
            HideMenu = false;
        }
        [RelayCommand]
        async void SavePlan()
        {
            int FandV = 0;
            int Starch = 0;
            int Dairy = 0;
            int Protein = 0;
            int FatSugar = 0;
            int Other = 0;
            foreach (string name in MealList)
            {
                if (name != "-") 
                {
                    var RecipeFromName = Meals.FirstOrDefault(recipe => recipe.Name == name);
                    FandV += RecipeFromName.Fruitandvegetables/RecipeFromName.Servings;
                    Starch += RecipeFromName.StarchyFood / RecipeFromName.Servings;
                    Dairy += RecipeFromName.Dairy / RecipeFromName.Servings;
                    Protein += RecipeFromName.Protein / RecipeFromName.Servings;
                    Other+= RecipeFromName.Other / RecipeFromName.Servings;
                }
            }
            Meal_Plan TempMealPlan = new Meal_Plan
            {
                Fruitandvegetables = FandV,
                StarchyFood = Starch,
                Dairy = Dairy,
                Protein = Protein,
                FatSugar = FatSugar,
                Other = Other,
                StartDate = StartDate,
                Duration = Duration,
                Meallist = string.Join(",", MealList)

            };
            MealPlanDb database = await MealPlanDb.Instance;
            await database.SaveAsync(TempMealPlan);
        }
    }
	
}