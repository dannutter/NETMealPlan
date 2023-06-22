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
		[ObservableProperty]
		ObservableCollection<string> selectedDate = new() ;
		[ObservableProperty]
		string selectSlot;
		[ObservableProperty]
		int duration =1 ;
		[ObservableProperty]
		string startDate;
		[ObservableProperty]
        ObservableCollection<MealSlot> mealSlots = new();
        public MealPlanViewModel()
		{
            StartDate = ((DateTime.Today).ToString())[..10];
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
                        MealSlots.Add(new MealSlot { Visible = "true",MealId =((i)+(X*7)).ToString()});
						if (X == 0)
						{
							SelectedDate.Add(((Dated.AddDays(i)).ToString())[..10]);
						}
					}
					else
					{
                        MealSlots.Add(new MealSlot { Visible = "false", MealId = ((i + 1) * (X + 1)).ToString() });
                    }
				}

			}
        }
		[RelayCommand]
		void SelectMenu(string param) 
		{
			SelectSlot = param;
        }
    }
	
}