using Crossword.Interafces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.Views
{
	public class TopicView:BindableObject,IMenageData<TopicView>
	{
		public static string FileName { get; } = "Topics.txt";
#if WINDOWS || ANDROID
		public static string FilePath { get; } = Path.Combine(FileSystem.AppDataDirectory, FileName);
#else
		public static string FilePath { get; } = Path.Combine(FileName);
#endif
		public static void SaveToFile(IEnumerable<TopicView> t,string filePath)
		{
			StreamWriter streamWriter = new StreamWriter(filePath);
			foreach (var topic in t)
			{
				streamWriter.WriteLine(topic.Topic);
			}
			streamWriter.Close();
		}
		public static void LoadFromString(string topicsString,ICollection<TopicView> t)
		{
			if (topicsString == null || topicsString == "") return;
			string[] topics = topicsString.Split("\n");
			foreach (var topic in topics)
			{
				if(topic != "")
					t.Add(new TopicView(topic.Trim('\r')));
			}
		}

		public static void Load(ICollection<TopicView> t, string filePath)
		{
			if (Path.Exists(filePath))
			{
				StreamReader streamReader = new StreamReader(filePath);
				string file = streamReader.ReadToEnd();
				streamReader.Close();
				LoadFromString(file,t);
			}
		}
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
		bool isChecked = false;

        public bool IsChecked
        {
            get => isChecked;
            set
            {
                isChecked = value;
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
		public override bool Equals(object? obj)
		{
			var other = obj as TopicView;
			if (other == null) return false;
			else
				return this.Topic == other.Topic;
		}
	}
}
