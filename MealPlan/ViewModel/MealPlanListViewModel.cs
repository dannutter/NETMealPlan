using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MealPlan.Services;
using System;
using static Microsoft.Maui.ApplicationModel.Permissions;
using System.Collections.ObjectModel;
using MealPlan.DataAccess;
using MealPlan.Model;

namespace MealPlan.ViewModel 
{
    public partial class MealPlanListViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<Meal_Plan> plans = new();
        public MealPlanListViewModel()
            {
            GetPlans();
            }

        async void GetPlans()
        {
            MealPlanDb database = await MealPlanDb.Instance;
            Plans = new ObservableCollection<Meal_Plan>((await database.GetRecipesAsync()) as List<Meal_Plan>);
        }
        private Meal_Plan selectPlan;
        public Meal_Plan SelectPlan
        {
            get { return selectPlan; }
            set
            {
                if (selectPlan != value)
                {
                    selectPlan = value;
                    OnPropertyChanged("SelectIngredient");
                    if (selectPlan != null)
                    {
                        Navigate(SelectPlan.Id);
                    }
                }
            }
        }
        async void Navigate(int Id)
        {
            await Shell.Current.GoToAsync(nameof(View.MealPlanPage));

            //await Shell.Current.GoToAsync($"{nameof(IngredientIndividualPage)}?Name={param}");
        }
    }
}