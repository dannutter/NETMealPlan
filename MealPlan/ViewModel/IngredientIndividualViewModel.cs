using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MealPlan.DataAccess;
using MealPlan.Model;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq.Expressions;

namespace MealPlan.ViewModel;
[QueryProperty(nameof(Name), nameof(Name))]
public partial class IngredientIndividualViewModel : ObservableObject
{

    public IngredientIndividualViewModel()
	{
    }
	[ObservableProperty]
	string name,photoPath;
    [ObservableProperty]
    string image1;

    [ObservableProperty]
    public Ingredient ingredient = new();
    [ObservableProperty]
    ObservableCollection<Page> imageArray = new();

    public class Page
    {

        public string ImageURL { get; set; }
        public string Pos { get; set; }
    }
    [RelayCommand]
    async void GetIngredient()
	{
        IngredientDb database = await IngredientDb.Instance;
        Ingredient = await database.GetAsync(Name);
        Image1 = Ingredient.Image1;
        ImageArray.Clear();
        ImageArray.Add(new Page { Pos="1st" ,ImageURL = Ingredient.Image1 });
        ImageArray.Add(new Page { Pos = "2nd", ImageURL = Ingredient.Image2 });
        ImageArray.Add(new Page { Pos = "3rd", ImageURL = Ingredient.Image3 });
    }
   
    [RelayCommand]
    async void TakePicture()
    {
        IngredientDb database = await IngredientDb.Instance;
        Ingredient = await database.GetAsync(Name);

        if (MediaPicker.Default.IsCaptureSupported)
        {
            var photo = await MediaPicker.Default.CapturePhotoAsync();
            if (photo != null)
            {
                var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                using (Stream stream = await photo.OpenReadAsync()) 
                using (var newStream = File.OpenWrite(newFile))
                {
                    await stream.CopyToAsync(newStream);
                }
                PhotoPath = newFile;
            }
        }
            Ingredient.Image1 = Ingredient.Image2;
            Ingredient.Image2 = Ingredient.Image3;
            Ingredient.Image3 = PhotoPath;
            await database.SaveAsync(Ingredient);
            GetIngredient();
    }
    
}