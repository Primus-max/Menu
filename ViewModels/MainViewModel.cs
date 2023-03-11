using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Menu.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        #region Заголовок окна
        private string _title = "Меню столовой ФКУ ИК - 3";
        /// <summary>  Заголовок окна </summary>
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
        #endregion

        #region Размер шрифта для заголовков блюд, выхода блюд и массы порций
        private double _titleInfoMenuFontResize = 18;
        /// <summary> Размер шрифта для заголовков блюд, выхода блюд и массы порций </summary>
        public double TitleInfoMenuFontSize
        {
            get => _titleInfoMenuFontResize;
            set => Set(ref _titleInfoMenuFontResize, value);

        }


        #endregion

        public class ActualHeightToFontSizeConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                double actualHeight = (double)value;
                return actualHeight / 30;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }

        }

        public MainViewModel()
        {
        }
    }
}
