using MealPlan.ViewModel;

namespace MealPlan.View;

public partial class IngredientIndividualPage : ContentPage
{
	public IngredientIndividualPage(IngredientIndividualViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
    protected override void OnAppearing()
	{
		if (BindingContext is  IngredientIndividualViewModel viewModel)
		{
			viewModel.GetIngredientCommand.Execute(null);
		}
        base.OnAppearing();
    }
    }