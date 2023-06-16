using MealPlan.Services;
using MealPlan.View;
using MealPlan.ViewModel;
using Microsoft.Extensions.Logging;

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

#if DEBUG
        builder.Logging.AddDebug();
#endif
        
        return builder.Build();
	}
}
