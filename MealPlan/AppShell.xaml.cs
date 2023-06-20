using MealPlan.View;

namespace MealPlan;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(CreateRecipePage), typeof(CreateRecipePage));
<<<<<<< Updated upstream
	}
=======
        Routing.RegisterRoute(nameof(IngredientDetailPage), typeof(IngredientDetailPage));
        Routing.RegisterRoute(nameof(IngredientIndividualPage), typeof(IngredientIndividualPage));
    }
>>>>>>> Stashed changes
}
