namespace MealPlan.View;

public partial class CreateRecipePage : ContentPage
{
	public CreateRecipePage(ViewModel.CreateRecipeViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}