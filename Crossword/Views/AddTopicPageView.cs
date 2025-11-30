
using Crossword.src;
using Crossword.Views.Interfaces;
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
                var excercie = TopicsToDelete.Find(ex => ex.Topic == e.Topic);

                if (excercie == null)
                    TopicsToDelete.Add(e);
                else
                    TopicsToDelete.Remove(excercie);
            });

            DeleteTopicCommand = new Command(() =>
            {
                UserDeletingTopics = !UserDeletingTopics;

                foreach (var excercie in TopicsToDelete)
                {
                    excercie.IsToRemove = false;
                    Topics.Remove(excercie);
                }

                TopicsToDelete.Clear();
            });
            DeleteTopicChangesCommand = new Command(() => Topics = new ObservableCollection<TopicView>(IAddTopicPageView.CopyTopicViews(AppData.Topics)));

            SaveTopicChangesCommand = new Command(() => AppData.SaveTopics(Topics));
            Topics = new ObservableCollection<TopicView>(IAddTopicPageView.CopyTopicViews(AppData.Topics));
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
		public List<TopicView> TopicsToDelete { get; set; } = new List<TopicView>();
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
				topic = "";
				foreach (var item in value.ToLower().Split(','))
				{
					topic += item;
				}
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
