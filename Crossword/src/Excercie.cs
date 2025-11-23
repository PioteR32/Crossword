using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.src
{
	public class Excercie : BindableObject
	{
		public int NumberOfQuestion { get; private set; } = 1;

		PasswordChar[] answer;
		public PasswordChar[] Answer
		{
			get => answer;

			set { answer = value; OnPropertyChanged(); }
		}
		string correctAnswer = "";
		public string CorrectAnswer
		{
			get => correctAnswer;

			set { correctAnswer = value; OnPropertyChanged(); }
		}
		private string question;
		public string Question
		{
			get => question;
			set
			{
				question = value; OnPropertyChanged();
			}
		}

		protected List<string> Topics { get; set; }
		bool isChecked = true;

		public bool IsChecked
		{
			get { return isChecked; }
			set { isChecked = value; OnPropertyChanged(); }
		}
		public Excercie(string question, string correctAnswer,params string[] topics)
		{
			Question = question;
			CorrectAnswer = correctAnswer;
			this.Topics = new List<string>(topics);
		}
		public Excercie(string question, string correctAnswer,IEnumerable<TopicView> topics)
		{
			Question = question;
			CorrectAnswer = correctAnswer;
			this.Topics = new List<string>(TopicsConverter(topics));
		}
		protected Excercie(string question, string correctAnswer, int correctAnswerLength, int numberOfQuestion)
		{
			Question = question;
			NumberOfQuestion = numberOfQuestion;
			CorrectAnswer = correctAnswer;
			answer = new PasswordChar[correctAnswerLength];
			for (int i = 0; i < answer.Length; i++)
			{
				answer[i] = new PasswordChar();
			}
		}
		public IEnumerable<string> TopicsConverter(IEnumerable<TopicView> topics)
		{
			List<string> returnValue = new List<string>();
			foreach (TopicView topicView in topics)
			{
				returnValue.Add(topicView.Topic);
			}
			return returnValue;
		}
		public override string ToString()
		{
			return $"{correctAnswer};{Question}";
		}
	}
}
