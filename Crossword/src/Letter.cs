using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Crossword.src
{
	public class Letter : BindableObject
	{
		public string Name { get; private set; } = "a";
		public char Character { get => Name[0]; }
		
		public Letter(string name)
		{
			Name = name;
			
		}
		
	}
}
