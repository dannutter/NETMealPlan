using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MealPlan.DataAccess;
using MealPlan.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MealPlan.ViewModel
{
    public partial class CreateRecipeViewModel : BaseViewModel
    {

       
        [ObservableProperty]
        ObservableCollection<string> ingredients;

        public CreateRecipeViewModel()
        {
            Ingredients = new ObservableCollection<string>();
        }
        [ObservableProperty]
        string recipeName, cook, prep, servings,instructions;

        [ObservableProperty]
        List<string> groups = new List<string>()
        {
            "Fruit and vegetables",
            "Starchy food",
            "Dairy",
            "Protein",
            "High in Fat/Sugar",
            "Other"
        };

        [ObservableProperty]
        string ingredientName, grams, group;
        [ObservableProperty]
        string photoPath;
        [ObservableProperty]
        Stream sourceStream;


        [RelayCommand]
        void SubIngredient()
        {
            if (string.IsNullOrWhiteSpace(IngredientName) || string.IsNullOrWhiteSpace(Grams) || string.IsNullOrWhiteSpace(Group))
            {
                return;
            }
            string Temp = IngredientName + "|" + Grams + "|" + Group;
            Ingredients.Add(Temp);
            IngredientName = Grams = Group = string.Empty;
        }

        [RelayCommand]
        void Delete(string r)
        { 
            if (Ingredients.Contains(r))
            {
                Ingredients.Remove(r);
            }
        }

        [RelayCommand]
        async void TakePicture()
        {
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

        }

        [RelayCommand]  
        async void Save()
        {
            int val = 0;
            IDictionary<string, int> IngredientGroup = new Dictionary<string, int>();
            IngredientGroup.Add("Fruit and vegetables", 0);
            IngredientGroup.Add("Starchy food", 1);
            IngredientGroup.Add("Dairy", 2);
            IngredientGroup.Add("Protein", 3);
            IngredientGroup.Add("High in Fat/Sugar", 4);
            IngredientGroup.Add("Other", 5);

            int[] GroupTotal = { 0, 0, 0, 0, 0, 0 };
            string Ingredientlist = string.Empty;
            string Ingredientamount = string.Empty;
            IngredientDb ingredientdb = await IngredientDb.Instance;
            foreach (string ingredient in Ingredients)
            {
                string[] splitIngredient = ingredient.Split('|');
                Ingredient Quer = await ingredientdb.GetAsync(splitIngredient[0]);
                if (Quer != null)
                {
                    Ingredientlist += Quer.Id + ",";
                }
                else
                {
                    Ingredient NewIngredient = new();
                    NewIngredient.Name = splitIngredient[0];
                    NewIngredient.Group = splitIngredient[2];
                    NewIngredient.Image1 = null;
                    NewIngredient.Image2 = null;
                    NewIngredient.Image3 = null;
                    await ingredientdb.SaveAsync(NewIngredient);
                    Quer = await ingredientdb.GetAsync(splitIngredient[0]);
                    Ingredientlist += Quer.Id + ",";
                }
                Int32.TryParse(splitIngredient[1], out val);
                GroupTotal[IngredientGroup[splitIngredient[2]]] += val;
                Ingredientamount += splitIngredient[1]+",";
            }
            Ingredientlist = Ingredientlist.Remove(Ingredientlist.Length - 1, 1);
            Ingredientamount = Ingredientamount.Remove(Ingredientamount.Length - 1, 1);
            Recipe Temp = new();
            Temp.Other = GroupTotal[5];
            Temp.FatSugar = GroupTotal[4];
            Temp.Protein = GroupTotal[3];
            Temp.Dairy = GroupTotal[2];
            Temp.StarchyFood = GroupTotal[1];
            Temp.Fruitandvegetables = GroupTotal[0];
            Temp.Name = RecipeName;
            Temp.Instructions = Instructions;
            Temp.Ingredients = Ingredientlist;
            Temp.Amounts = Ingredientamount;
            Temp.Image = PhotoPath;
            val = 0;
            Int32.TryParse(Cook,out val);
            Temp.CookTime = val;
            val = 0;
            Int32.TryParse(Prep, out val);
            Temp.PrepTime = val;
            val = 0;
            Int32.TryParse(Servings, out val);
            Temp.Servings = val;
            CreateRecipeDb database = await CreateRecipeDb.Instance;
            await database.SaveAsync(Temp);

        }
    }
}
