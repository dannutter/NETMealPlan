namespace MealPlan.View;

public partial class MainPage : ContentPage
{
    public MainPage(ViewModel.MainPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}

