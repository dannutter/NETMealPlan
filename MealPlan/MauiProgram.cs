using MealPlan.Services;
using MealPlan.View;
using MealPlan.ViewModel;
using MealPlan.Model;
using Microsoft.Extensions.Logging;
using MealPlan.DataAccess;

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
        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<CreateRecipePage>();
        builder.Services.AddSingleton<CreateRecipeViewModel>();
        builder.Services.AddSingleton<IngredientDetailPage>();
        builder.Services.AddSingleton<IngredientDetailViewModel>();
        builder.Services.AddTransient<IngredientIndividualViewModel>();
        builder.Services.AddTransient<IngredientIndividualPage>();
        builder.Services.AddSingleton<CreateRecipeDb>();
        builder.Services.AddSingleton<Recipe>();
        builder.Services.AddSingleton<Ingredient>();
        builder.Services.AddSingleton<IngredientDb>();
        builder.Services.AddTransient<Ingredient>();
        builder.Services.AddTransient<IngredientDb>();
        builder.Services.AddTransient<MealPlanViewModel>();
        builder.Services.AddTransient<MealPlanPage>();



#if DEBUG
        builder.Logging.AddDebug();
#endif
        
        return builder.Build();
	}
}
