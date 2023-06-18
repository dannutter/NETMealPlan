using MealPlan.DataAccess;
using MealPlan.ViewModel;

namespace MealPlan.View;

public partial class CreateRecipePage : ContentPage
{

    public CreateRecipePage(CreateRecipeViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}