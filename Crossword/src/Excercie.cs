
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Crossword.src
{
	public class Excercie
	{
		public int NumberOfQuestion { get;  set; } = 1;


		public string correctAnswer = "";
		
		public string question  = "";
		public int CorrectAnswerLenght { get; set; } = 0;
		public List<string> Topics { get; set; }


		public Excercie(string question, string correctAnswer,params string[] topics)
		{
			this.question = question;
			this.correctAnswer = correctAnswer;
			this.Topics = new List<string>(topics);
		}
		public Excercie(string question, string correctAnswer, int numberOfQuestion, params string[] topics)
		{
			this.question = question;
			this.correctAnswer = correctAnswer;
			this.Topics = new List<string>(topics);
			NumberOfQuestion = numberOfQuestion;
		}
		public Excercie(string question, string correctAnswer, int numberOfQuestion)
		{
			this.question = question;
			this.correctAnswer = correctAnswer;
			NumberOfQuestion = numberOfQuestion;
		}

		protected Excercie(string question, string correctAnswer, int correctAnswerLength, int numberOfQuestion)
		{
			this.question = question;
			NumberOfQuestion = numberOfQuestion;
			this.correctAnswer = correctAnswer;
			CorrectAnswerLenght = correctAnswerLength;
		}

		public override string ToString()
		{
			string topicsString = string.Join(",", Topics);
			return $"{correctAnswer};{question};{topicsString}";
		}
		public static bool operator  ==(Excercie a, Excercie b)
		{
			if (a is null || b is null)
				return a as object == b as object;
			return a.correctAnswer == b.correctAnswer 
				&& a.question == b.question 
				&& a.Topics.SequenceEqual(b.Topics) 
				&& a.NumberOfQuestion == b.NumberOfQuestion;
		}
		public static bool operator  !=(Excercie a, Excercie b)
		{
			if (a is null || b is null)
				return a as object != b as object;
			return !(a.correctAnswer == b.correctAnswer
				&& a.question == b.question
				&& a.Topics.SequenceEqual(b.Topics)
				&& a.NumberOfQuestion == b.NumberOfQuestion);
		}
		public override bool Equals(object? obj)
		{
			Excercie other = obj as Excercie;
			if (other is null)
				return false;
			if(other == this as Excercie)
				return true;
			else
				return false;
		}
	}
}
