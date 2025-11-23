
using Crossword.src;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
namespace Crossword.Views
{
	

	public partial class MainPageView : ObservableObject
	{
		[RelayCommand]
		private async Task GoToAddCrosswordPageKeys()
		{
			await Shell.Current.GoToAsync(nameof(Crossword.Pages.AddCrosswordKeysPage));
		}
		[RelayCommand]
		private async Task GoToCrosswordPage()
		{
			await Shell.Current.GoToAsync(nameof(Crossword.Pages.CrosswordPage));
		}
	}

}

