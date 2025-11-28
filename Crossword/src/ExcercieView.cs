using Crossword.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Crossword.src
{
    public class ExcercieView : Excercie, IAddTopicPageView
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
        ObservableCollection<TopicView> topics = new ObservableCollection<TopicView>();
        public ObservableCollection<TopicView> Topics
        {
            get => topics;
            set
            {
                topics = value;
                OnPropertyChanged();
            }
        }

        public List<TopicView> TopicsToDelete { get; set; }
        bool userDeletingTopics = false;
        public bool UserDeletingTopics
        {
            get => userDeletingTopics; 
            set
            {
                userDeletingTopics = value;
                OnPropertyChanged();
            }
        }
        public void AddToDeleteCollection(TopicView e)
        {
            e.IsToRemove = !e.IsToRemove;
            var excercie = TopicsToDelete.Find(ex => ex.Topic == e.Topic);

            if (excercie == null)
                TopicsToDelete.Add(e);
            else
                TopicsToDelete.Remove(excercie);
        }
        public void DeleteTopic()
        {
            UserDeletingTopics = !UserDeletingTopics;

            foreach (var excercie in TopicsToDelete)
            {
                excercie.IsToRemove = false;
                Topics.Remove(excercie);
            }

            TopicsToDelete.Clear();
        }
        public void AddTopic(TopicView e)
        {
            if (Topic == "" || Topic == null) return;

            if (!Topics.Any(e => e.Topic == Topic.ToLower()))
            {
                Topics.Add(new TopicView(Topic.ToLower()));
            }
        }
        public void DeleteTopicChanges()
        {
            Topics = new (CopyTopics);
        }
        public string Topic { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ICommand AddTopicCommand { get; set; }
        public ICommand DeleteTopicCommand { get; set; }
        public ICommand AddToDeleteTopicCommand { get; set; }
        public ICommand SaveTopicChangesCommand { get; set; }
        public ICommand DeleteTopicChangesCommand { get; set; }
        public ObservableCollection<TopicView> CopyTopics { get; set; }

        void CreateFunction()
        {
            AddTopicCommand = new Command<TopicView>(AddTopic);
            AddToDeleteTopicCommand = new Command<TopicView>(AddToDeleteCollection);
            DeleteTopicCommand = new Command(DeleteTopic);
            DeleteTopicChangesCommand = new Command(DeleteTopicChanges);
            SaveTopicChangesCommand = new Command(() => CopyTopics = new(Topics));
        }
        public ExcercieView(string question, string correctAnswer, int startPositionInGrid) : base(question, correctAnswer, startPositionInGrid + correctAnswer.Length, 1)
        {
            CreateFunction();
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
            CreateFunction();
        }
        public ExcercieView(string question, string correctAnswer, params string[] topics) : base(question, correctAnswer, topics)
        {
            CreateFunction();
            for (int i = 0; i < topics.Length; i++)
            {
                Topics.Add(new (topics[i]));
            }
        }
        public ExcercieView(string question, string correctAnswer, IEnumerable<TopicView> topics) : base(question, correctAnswer, topics)
        {
            CreateFunction();
            foreach (var topic in topics) 
            {
                Topics.Add(new TopicView(topic.Topic));
            }
        }
        public ExcercieView(string question, string correctAnswer, IEnumerable<string> topics) : base(question, correctAnswer, topics)
        {
            CreateFunction();
            foreach (var topic in topics) 
            {
                Topics.Add(new TopicView(topic));
            }
        }
        public ExcercieView(ExcercieView ev) : base(ev.Question, ev.CorrectAnswer, ev.Topics)
        {
            CreateFunction();
            Topics.CollectionChanged += OnTopicsViewCollectionChanged;
        }

        void OnTopicsViewCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            TopicView newTopic = (TopicView)e.NewItems[0];
            TopicView oldTopic = (TopicView)e.OldItems[0];
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    base.Topics.Add(newTopic.Topic);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    base.Topics.Remove(newTopic.Topic);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    for (int i = 0; i < base.Topics.Count; i++)
                    {
                        if (base.Topics[i] == oldTopic.Topic)
                        {
                            base.Topics[i] = newTopic.Topic;
                            return;
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    base.Topics.Clear();
                    break;
            }
        }
    }
}
