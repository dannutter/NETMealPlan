using CommunityToolkit.Mvvm.ComponentModel;
using Java.Nio.Channels;
using System;

namespace MealPlan.ViewModel;

public partial class RecipeDetailViewModel : ContentPage
{
    [ObservableProperty]
    Image wimage = new();



    void Dorecipe()
    {

    }

    Source = ImageSource.FromStream(test);
}