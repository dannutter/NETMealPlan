using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MealPlan.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MealPlan.ViewModel 
{

    public partial class MainPageViewModel : ObservableObject
    {
        private readonly ISecureStorageService secureStorageService;

        public MainPageViewModel(ISecureStorageService secureStorageService)
        {
            this.secureStorageService = secureStorageService;
        }
        [ObservableProperty]
        string pin;
        [ObservableProperty]
        string loginError;
        [RelayCommand]
        private async Task SavePin(string savePin)
        {
            await secureStorageService.Save("pin", savePin);
            LoginError = "Pin Saved";
        }
        [RelayCommand]
        private async Task CheckPin()
        {
            var checkedPin = await secureStorageService.Get(Pin);
            LoginError = checkedPin;
            if (checkedPin == "null")
            {
                await SavePin(Pin);
                Navigate();
            }
            else if (checkedPin == "true")
            {
                LoginError = "Success";
                Navigate();
            }
            else
            {
                LoginError = "Failed to Login";
            }

        }
        [RelayCommand]
        void PinButton(string pinNo)
        {
            Pin += pinNo;
        }

        [RelayCommand]
        void DelButton()
        {
            if (Pin.Length > 0)
            {
                Pin = Pin.Remove(Pin.Length - 1, 1);
            }
        }
        private async void Navigate()
        {
            await Shell.Current.GoToAsync(nameof(View.MealPlanListPage));
        }
    }
}