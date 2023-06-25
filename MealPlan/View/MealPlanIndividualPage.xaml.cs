using MealPlan.ViewModel;

namespace MealPlan.View;

public partial class MealPlanIndividualPage : ContentPage
{
    public MealPlanIndividualPage(MealPlanIndividualViewModel viem)
    {
        InitializeComponent();
        BindingContext = viem;
    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
    protected override void OnAppearing()
    {
        if (BindingContext is MealPlanIndividualViewModel viewModel)
        {
            viewModel.GetDataCommand.Execute(null);
        }
        base.OnAppearing();
    }
}