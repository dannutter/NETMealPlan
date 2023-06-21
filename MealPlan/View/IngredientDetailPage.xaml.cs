using MealPlan.ViewModel;
using Microsoft.Maui.Controls;
namespace MealPlan.View;

public partial class IngredientDetailPage: ContentPage
{
    IngredientDetailViewModel ingredientDetailViewModel;
    public IngredientDetailPage()
	{
		InitializeComponent();
		BindingContext = ingredientDetailViewModel = new IngredientDetailViewModel();


    }
	void Entry_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
	{
        if (string.IsNullOrWhiteSpace(e.NewTextValue))
            IngredientColl.ItemsSource = ingredientDetailViewModel.Ingredients;
        else
            IngredientColl.ItemsSource = ingredientDetailViewModel.Ingredients.Where(X => X.Name.ToLower().Contains(e.NewTextValue.ToLower()));
    }
}
