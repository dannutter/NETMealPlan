using MealPlan.View;

namespace MealPlan;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(CreateRecipePage), typeof(CreateRecipePage));
	}
}
