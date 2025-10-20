using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.src
{
	public class PasswordChar:BindableObject
	{
		char character = '\0';
		public char Character
		{
			get => character; 
			set { character = value; OnPropertyChanged(); }
		}
		public int Index { get; set; } = -1;
	}
}
