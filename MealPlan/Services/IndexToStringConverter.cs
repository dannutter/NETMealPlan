using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPlan.Services
{
    public class IndexToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not bool isVisible)
                return Binding.DoNothing;

            var collectionView = (CollectionView)parameter;
            var visibleItems = collectionView.ItemsSource as ObservableCollection<bool>;
            var index = visibleItems?.IndexOf(isVisible);

            return index != null && index >= 0 ? index.ToString() : Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
