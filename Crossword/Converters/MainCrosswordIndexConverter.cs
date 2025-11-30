using Crossword.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.Converters
{
	public class IndexEqualsConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var index = (int)value;
			var view = parameter as Page;
			var tmp = view.BindingContext as CrosswordPageView;
			return index == tmp?.PasswordMainLettersIndex;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> throw new NotImplementedException();
	}

}
