using MealPlan.ViewModel;
using Microsoft.Maui.Controls;
namespace MealPlan.View;

public partial class MealPlanPage : ContentPage
{
    public MealPlanPage(MealPlanViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;


    }
}
