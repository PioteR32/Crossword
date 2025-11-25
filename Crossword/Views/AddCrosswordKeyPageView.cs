using Crossword.src;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Crossword.Views
{
	public class AddCrosswordKeyPageView : AddTopicPageView
	{
		public AddCrosswordKeyPageView():base()
		{
			AddEcxcercieCommand = new Command(() =>
			{
				if (Question == "" || Question == null || Answer == "" || Answer == null) return;

				if (!Excercies.Any(e => e.CorrectAnswer == Answer.ToLower()
					|| e.Question == Question.ToLower()))
				{
					Excercies.Add(new ExcercieView(Question.ToLower(), Answer.ToLower()));
				}
			});
			AddToDeleteEcxcercieCommand = new Command<ExcercieView>((e) =>
			{
				e.IsToRemove = !e.IsToRemove;
				var excercie = ExcerciesesToDelete.Find(ex => ex.CorrectAnswer == e.CorrectAnswer
					&& e.Question == ex.Question);
				
				if (excercie == null)
					ExcerciesesToDelete.Add(e);
				else
					ExcerciesesToDelete.Remove(excercie);
			});

			DeleteEcxcerciesCommand = new Command(() =>
			{
				UserDeletingExcercies = !UserDeletingExcercies;

				foreach (var excercie in ExcerciesesToDelete)
				{
					excercie.IsToRemove = false;
					Excercies.Remove(excercie);
				}

				ExcerciesesToDelete.Clear();
			});
			DeleteChangesCommand = new Command(async () => Excercies = new ObservableCollection<ExcercieView>(CopyExcercieViews(AppData.Excercies)));

			SaveChangesCommand = new Command(() => AppData.SaveExcercies(Excercies));

			Excercies = new ObservableCollection<ExcercieView>(CopyExcercieViews(AppData.Excercies));
		}
		static IEnumerable<ExcercieView> CopyExcercieViews(IEnumerable<ExcercieView> excercies)
		{
			List<ExcercieView> returnList = new List<ExcercieView>();
			foreach (var view in excercies)
			{
				returnList.Add(new ExcercieView(view));
			}
			return returnList;
		}
		
		public bool userDeletingExcercies = false;
		public bool UserDeletingExcercies
		{
			get => userDeletingExcercies;
			set
			{
				userDeletingExcercies = value;
				OnPropertyChanged();
			}
		}
		public List<ExcercieView> ExcerciesesToDelete = new List<ExcercieView>();
		private ObservableCollection<ExcercieView> excercies;
		public ObservableCollection<ExcercieView> Excercies 
		{
			get => excercies;
			private set
			{
				excercies = value;
				OnPropertyChanged();
			}
		} 
		string question;
		public string Question
		{
			get => question;
			set
			{
				question = value.Trim(Global.BlankLetterChar);
				OnPropertyChanged();
			}
		}

		string answer;
		public string Answer
		{
			get => answer;
			set
			{
				answer = value.Trim(Global.BlankLetterChar);
				OnPropertyChanged();
			}
		}
		public ICommand AddEcxcercieCommand { get; set; }
		public ICommand DeleteEcxcerciesCommand { get; set; }
		public ICommand AddToDeleteEcxcercieCommand { get; set; }
		public ICommand SaveChangesCommand { get; set; }
		public ICommand DeleteChangesCommand { get; set; }
	}
}
