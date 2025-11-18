
using Crossword.src;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Crossword.Views
{
	public class MainPageView:BindableObject
	{
		public ObservableCollection<ExcercieView> Excercies { get; private set; }
		public ObservableCollection<LetterView> Letters { get; private set; }
		ExcercieView selectedExcercie = null;
		public int TotalExcercies
		{
			get => Excercies.Count;
		}
		public int NumberOfGridColumns
		{
			get; private set;
		}
		public ExcercieView SelectedExcercie
		{
			get => selectedExcercie;
			set
			{
				selectedExcercie = value;
				OnPropertyChanged();
			}
		}
		public MainPageView()
		{
			FocuseButton = new Command<ExcercieView>(
			(ExcercieView excercie) => {
				foreach (ExcercieView exc in Excercies)
				{
					exc.IsChecked = false;
				}
				excercie.IsChecked = true;
				SelectedExcercie = excercie;
			});
			Excercies = new ObservableCollection<ExcercieView> { new ExcercieView("test", "atest",0), new ExcercieView("test", "atest",0) };
			Letters = new ObservableCollection<LetterView> {
				new LetterView("q",0,0),
				new LetterView("w", 0, 1),
				new LetterView("e",0,2),
				new LetterView("r", 0, 3),
				new LetterView("t",0,4),
				new LetterView("y", 0, 5),
				new LetterView("u",0,6),
				new LetterView("i", 0, 7),
				new LetterView("o",0,8),
				new LetterView("p", 0, 9),

				new LetterView("a",1,0),
				new LetterView("s", 1, 1),
				new LetterView("d",1,2),
				new LetterView("f", 1, 3),
				new LetterView("g",1,4),
				new LetterView("h", 1, 5),
				new LetterView("j",1,6),
				new LetterView("k", 1, 7),
				new LetterView("l",1,8),

				new LetterView("z", 2, 0),
				new LetterView("x",2,1),
				new LetterView("c", 2, 2),
				new LetterView("v",2,3),
				new LetterView("b", 2, 4),
				new LetterView("n",2,5),
				new LetterView("m", 2, 6),
				new LetterView("<--", 2, 7),
			};
			
			Clickced = new Command<LetterView>(AddLetter);
			NumberOfGridColumns = 10;
		}
		public ICommand Clickced { get; private set; }
		public ICommand FocuseButton { get; private set; }
		public ICommand CheckAnswers { get; private set; }
		int goodAnswers = 0;
		public int GoodAnswers
		{
			get => goodAnswers; set { goodAnswers = value; OnPropertyChanged(); }
		}
		void CheckAllAnswers()
		{
			foreach (Excercie exc in Excercies)
			{
				string ans = "";
				foreach (PasswordChar pc in exc.Answer)
				{
					ans += pc.Character;
				}

				if (ans == exc.CorrectAnswer)
					GoodAnswers++;
			}
		}
		void AddLetter(LetterView letter)
		{
			ExcercieView excercie = SelectedExcercie;
			Letters[0] = new LetterView("a", Letters[0].X, Letters[0].Y);
			if (excercie != null)
			{
				if (excercie.IsChecked == true)
				{
					if (letter.Name == "<--")
					{
						if (excercie.ActualPositionInTable != 0)
							excercie.Answer[excercie.ActualPositionInTable - 1].Character = '\0';
					}
					else if (excercie.ActualPositionInTable < excercie.CorrectAnswer.Length)
					{
						excercie.Answer[excercie.ActualPositionInTable].Character = letter.Character;
					}
				}

			}
		}
	}
}

