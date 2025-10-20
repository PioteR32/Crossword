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
		char Character { get => Name[0]; }
		public ICommand Clickced { get; private set; }
		public Letter(string name)
		{
			Name = name;
			Clickced = new Command<ExcercieView>(AddLetter);
		}
		void AddLetter(ExcercieView excercie)
		{
			if (excercie != null)
			{
				if (excercie.IsChecked == true)
				{
					if (Name == "<--")
					{
						if (excercie.ActualPositionInTable != 0)
							excercie.Answer[excercie.ActualPositionInTable - 1].Character = '\0';
					}
					else if (excercie.ActualPositionInTable < excercie.CorrectAnswer.Length)
					{
						excercie.Answer[excercie.ActualPositionInTable].Character = Character;
					}
				}

			}
		}
	}
}
