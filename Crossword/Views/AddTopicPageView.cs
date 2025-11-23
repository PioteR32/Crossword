using Crossword.src;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Crossword.Views
{
	public class AddTopicPageView:BindableObject
	{
		public AddTopicPageView()
		{
			Excercies = new ObservableCollection<TopicView>(CopyTopicViews(AppData.Topics));
		}
		static IEnumerable<TopicView> CopyTopicViews(IEnumerable<TopicView> excercies)
		{
			List<TopicView> returnList = new List<TopicView>();
			foreach (var view in excercies)
			{
				returnList.Add(new TopicView(view.Topic));
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
		public List<TopicView> ExcerciesesToDelete = new List<TopicView>();
		private ObservableCollection<TopicView> excercies;
		public ObservableCollection<TopicView> Excercies
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
		string topics;
		public string Topics
		{
			get => topics;
			set
			{
				topics = value;
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
