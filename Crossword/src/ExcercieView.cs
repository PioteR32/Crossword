using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.src
{
	public class ExcercieView:Excercie
	{
		int startPositionInGrid = 0;
		public int StartPositionInGrid
		{
			get => startPositionInGrid; 
			set { startPositionInGrid = value; OnPropertyChanged(); }
		}
		public int GridColumnCount
		{
			get => startPositionInGrid + CorrectAnswer.Length;
		}
		public int ActualPositionInTable
		{
			get
			{
				for (int i = 0; i < Answer.Length; i++)
				{
					if (Answer[i].Character == '\0')
					{
						return i;
					}
				}
				return CorrectAnswer.Length;
			}
		}
		
		public ExcercieView(string question, string correctAnswer,int startPositionInGrid):base(question, correctAnswer)
		{
			StartPositionInGrid = startPositionInGrid;
			for (int i=0;i< CorrectAnswer.Length;i++)
			{
				Answer[i].Index = i + StartPositionInGrid;
			}
			
		}
	}
}
