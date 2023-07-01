using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MealPlan.DataAccess;
using MealPlan.Model;
using MealPlan.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPlan.ViewModel;

public class IngredientDetailViewModel : BaseViewModel
{

    public IngredientDetailViewModel ()
    {
        GetallIngredients ();
    }
    private ObservableCollection<Ingredient> ingredients;
    public ObservableCollection<Ingredient> Ingredients {
        get
        {
            return ingredients;
        }
        set 
        {
                ingredients = value;
                OnPropertyChanged("Ingredients");
        }
    }

    async void GetallIngredients()
    {
        IngredientDb database = await IngredientDb.Instance;
        Ingredients = new ObservableCollection<Ingredient>((await database.GetIngAsync()) as List<Ingredient>);
    }

    private Ingredient selectIngredient;
    public Ingredient SelectIngredient
    {
        get { return selectIngredient; }
        set
        {
            if (selectIngredient != value)
            {
                selectIngredient = value;
                OnPropertyChanged("SelectIngredient");
                if (selectIngredient != null)
                {
                    ParamLink(SelectIngredient.Name);
                }
            }
        }
    }
    private async void ParamLink(string param)
    {
        await Shell.Current.GoToAsync($"{nameof(IngredientIndividualPage)}?Name={param}");
    }
}