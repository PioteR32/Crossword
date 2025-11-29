using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.Views
{
	public class CrosswordView:Crossword.src.Crossword, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		public CrosswordView(string name) : base(name)
		{
		}
		public CrosswordView(Crossword.src.Crossword crossword) : base(crossword)
		{
		}

	}
}
