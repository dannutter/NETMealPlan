using MealPlan.Services;
using MealPlan.View;

namespace MealPlan
{

    public partial class AppShell : Shell
    {

        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CreateRecipePage), typeof(CreateRecipePage));
            Routing.RegisterRoute(nameof(IngredientDetailPage), typeof(IngredientDetailPage));
            Routing.RegisterRoute(nameof(IngredientIndividualPage), typeof(IngredientIndividualPage));
            Routing.RegisterRoute(nameof(MealPlanPage), typeof(MealPlanPage));
            Routing.RegisterRoute(nameof(MealPlanListPage), typeof(MealPlanListPage));
            Routing.RegisterRoute(nameof(MealPlanIndividualPage), typeof(MealPlanIndividualPage));
        }
    }
}