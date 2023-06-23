namespace MealPlan.View;

public partial class MealPlanListPage : ContentPage
{
    public MealPlanListPage(ViewModel.MealPlanListViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}

