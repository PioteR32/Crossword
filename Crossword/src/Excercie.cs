using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.src
{
    public class Excercie:BindableObject
    {
		public string Question {  get; private set; }
        PasswordChar[] answer ;
		string correctAnswer = "";

        public PasswordChar[] Answer
        {
            get => answer;

            set { answer = value; OnPropertyChanged(); }
        } 
        public string CorrectAnswer
        {
            get => correctAnswer;

            set { correctAnswer = value; OnPropertyChanged(); }
        }
        bool isChecked = true;

		public bool IsChecked
        { 
            get { return isChecked; } 
            set { isChecked = value; OnPropertyChanged(); }
        }
        public Excercie(string question,string correctAnswer)
        { 
            Question = question;
            CorrectAnswer = correctAnswer;
            answer = new PasswordChar[correctAnswer.Length];
            for(int i = 0; i < answer.Length; i++)
            {
                answer[i] = new PasswordChar();
			}
		}
    }
}
