using Crossword.src;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Crossword.Views.Interfaces
{
    internal interface IAddTopicPageView
    {
        public ObservableCollection<TopicView> Topics { get; set; }
        public ObservableCollection<TopicView> CopyTopics { get; set; }
        public List<TopicView> TopicsToDelete { get; set; }
        public bool UserDeletingTopics { get; set; }
        public string Topic {  get; set; }
        public static IEnumerable<TopicView> CopyTopicViews(IEnumerable<TopicView> excercies)
        {
            List<TopicView> returnList = new List<TopicView>();
            foreach (var view in excercies)
            {
                returnList.Add(new TopicView(view.Topic));
            }
            return returnList;
        }
      
        public ICommand AddTopicCommand { get; set; }
        public ICommand DeleteTopicCommand { get; set; }
        public ICommand AddToDeleteTopicCommand { get; set; }
        public ICommand SaveTopicChangesCommand { get; set; }
        public ICommand DeleteTopicChangesCommand { get; set; }
    }
}
