using MealPlan.Services;
using MealPlan.Interfaces;
using MealPlan.View;
using MealPlan.ViewModel;
using MealPlan.Model;
using Microsoft.Extensions.Logging;
using MealPlan.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace MealPlan;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddSingleton<ISecureStorageService, SecureStorageService>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<BaseViewModel>();
        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<CreateRecipePage>();
        builder.Services.AddSingleton<CreateRecipeViewModel>();
        builder.Services.AddSingleton<IngredientDetailPage>();
        builder.Services.AddSingleton<IngredientDetailViewModel>();
        builder.Services.AddSingleton<IngredientIndividualViewModel>();
        builder.Services.AddSingleton<IngredientIndividualPage>();
        builder.Services.AddSingleton<CreateRecipeDb>();
        builder.Services.AddScoped<Recipe>();
        builder.Services.AddSingleton<Ingredient>();
        builder.Services.AddSingleton<IngredientDb>();
        builder.Services.AddSingleton<Ingredient>();
        builder.Services.AddSingleton<IngredientDb>();
        builder.Services.AddSingleton<MealPlanViewModel>();
        builder.Services.AddSingleton<MealPlanPage>();
        builder.Services.AddSingleton<Meal_Plan>();
        builder.Services.AddSingleton<MealPlanDb>();
        builder.Services.AddSingleton<MealPlanListViewModel>();
        builder.Services.AddScoped<MealPlanListPage>();
        builder.Services.AddTransient<MealPlanIndividualPage>();
        builder.Services.AddScoped<MealPlanIndividualViewModel>();



#if DEBUG
        builder.Logging.AddDebug();
#endif
        
        return builder.Build();
	}
}
