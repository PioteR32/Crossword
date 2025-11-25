
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
            AddTopicCommand = new Command(() =>
            {
                if (Topic == "" || Topic == null ) return;

                if (!Topics.Any(e => e.Topic == Topic.ToLower()))
                {
                    Topics.Add(new TopicView(Topic.ToLower()));
                }
            });
            AddToDeleteTopicCommand = new Command<TopicView>((e) =>
            {
                e.IsToRemove = !e.IsToRemove;
                var excercie = ExcerciesesToDelete.Find(ex => ex.Topic == e.Topic);

                if (excercie == null)
                    ExcerciesesToDelete.Add(e);
                else
                    ExcerciesesToDelete.Remove(excercie);
            });

            DeleteTopicCommand = new Command(() =>
            {
                UserDeletingTopics = !UserDeletingTopics;

                foreach (var excercie in ExcerciesesToDelete)
                {
                    excercie.IsToRemove = false;
                    Topics.Remove(excercie);
                }

                ExcerciesesToDelete.Clear();
            });
            DeleteTopicChangesCommand = new Command(() => Topics = new ObservableCollection<TopicView>(CopyTopicViews(AppData.Topics)));

            SaveTopicChangesCommand = new Command(() => AppData.SaveTopics(Topics));
            Topics = new ObservableCollection<TopicView>(CopyTopicViews(AppData.Topics));
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

		public bool userDeletingTopics = false;
		public bool UserDeletingTopics
		{
			get => userDeletingTopics;
			set
			{
				userDeletingTopics = value;
				OnPropertyChanged();
			}
		}
		public List<TopicView> ExcerciesesToDelete = new List<TopicView>();
		private ObservableCollection<TopicView> topics;
		public ObservableCollection<TopicView> Topics
		{
			get => topics;
			private set
			{
				topics = value;
				OnPropertyChanged();
			}
		}
		string topic;
		public string Topic
		{
			get => topic;
			set
			{
				topic = value;
				OnPropertyChanged();
			}
		}

		public ICommand AddTopicCommand { get; set; }
		public ICommand DeleteTopicCommand { get; set; }
		public ICommand AddToDeleteTopicCommand { get; set; }
		public ICommand SaveTopicChangesCommand { get; set; }
		public ICommand DeleteTopicChangesCommand { get; set; }
	}
}
