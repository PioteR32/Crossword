using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.src
{
	public class ExcercieView : Excercie
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

		private bool isToRemove = false;
		public bool IsToRemove
		{
			get => isToRemove;
			set
			{
				isToRemove = value;
				OnPropertyChanged();
			}
		}
		public int ActualPositionInTable
		{
			get
			{
				for (int i = 0; i < GridColumnCount; i++)
				{
					if (Answer[i].Character == '\0')
					{
						return i;
					}
				}
				return GridColumnCount;
			}
		}
		public ObservableCollection<TopicView> TopicsViews { get; set; }
		public ExcercieView(string question, string correctAnswer, int startPositionInGrid) : base(question, correctAnswer, startPositionInGrid + correctAnswer.Length, 1)
		{
			StartPositionInGrid = startPositionInGrid;
			for (int i = 0; i < GridColumnCount; i++)
			{
				Answer[i].Index = i;
				if (i < StartPositionInGrid)
				{
					Answer[i].Character = Global.BlankLetterChar;
				}
			}
		}
		public ExcercieView(string question, string correctAnswer) : base(question, correctAnswer)
		{

		}
		public ExcercieView(string question, string correctAnswer, params string[] topics) : base(question, correctAnswer, topics)
		{

		}
		public ExcercieView(ExcercieView ev) : base(ev.Question, ev.CorrectAnswer, ev.TopicsViews)
		{
			TopicsViews.CollectionChanged += OnTopicsViewCollectionChanged;
		}

		void OnTopicsViewCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{

			TopicView newTopic = (TopicView)e.NewItems[0];
			TopicView oldTopic = (TopicView)e.OldItems[0];
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					Topics.Add(newTopic.Topic);
					break;
				case NotifyCollectionChangedAction.Remove:
					Topics.Remove(newTopic.Topic);
					break;
				case NotifyCollectionChangedAction.Replace:
					for (int i = 0; i < Topics.Count; i++)
					{
						if (Topics[i] == oldTopic.Topic)
						{
							Topics[i] = newTopic.Topic;
							return;
						}
					}
					break;
				case NotifyCollectionChangedAction.Reset:
					Topics.Clear();
					break;
			}
		}
	}
}
