namespace MealPlan;

public partial class App : Application
{
	public App(View.MainPage page)
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
