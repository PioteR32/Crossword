using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Crossword.src
{
	public class LetterView: Letter
	{
		int x = 0;
		public int X
		{
			get => x; set { x = value; OnPropertyChanged(); }
		}
		int y = 0;
		public int Y {  
				get => y; set { y = value; OnPropertyChanged(); }
			
		}
		public LetterView(string name,int y, int x):base(name)
		{
			X = x;
			Y = y;			
		}
	}
}
