using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MealPlan.Services;
using MealPlan.Interfaces;
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

    public partial class BaseViewModel: ObservableObject
    {
        public bool Theme = true;
        [RelayCommand]
        void ThemeButton()
        {
            if (Theme) 
            {
                Application.Current.UserAppTheme = AppTheme.Dark;
                Theme = false;
            }
            else
            {
                Application.Current.UserAppTheme = AppTheme.Light;
                Theme = true;
            }
        }

    }
}