using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.src
{
	public class TopicView:BindableObject
	{
		public string Topic { get; set; }
		public TopicView(string topic) { Topic = topic; }
		bool isToRemove = false;
        public bool IsToRemove 
		{
			get => isToRemove;
			set
			{
				isToRemove = value;
				OnPropertyChanged();
			}
		}
		bool isVisibleButton = false;
		public bool IsVisibleButton
		{
			get => isVisibleButton;
			set
			{
				isVisibleButton = value; OnPropertyChanged();
			}
		}
	}
}
