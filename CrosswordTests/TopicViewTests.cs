using Crossword.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordTests
{
	public class TopicViewTests
	{
		[Fact]
		public void LoadFromString_AddsTopics_WhenValidString()
		{
			var coll = new List<TopicView>();
			string src = "alpha\nbeta\r\ngamma\n";
			TopicView.LoadFromString(src, coll);

			Assert.Equal(3, coll.Count);
			Assert.Equal("alpha", coll[0].Topic);
			Assert.Equal("beta", coll[1].Topic);
			Assert.Equal("gamma", coll[2].Topic);
		}

		[Fact]
		public void LoadFromString_DoesNothing_WhenNullOrEmpty()
		{
			var coll = new List<TopicView> { new TopicView("existing") };
			TopicView.LoadFromString(null!, coll);
			TopicView.LoadFromString(string.Empty, coll);

			Assert.Single(coll);
			Assert.Equal("existing", coll[0].Topic);
		}

		[Fact]
		public void SaveToFile_WritesOneTopicPerLine_And_LoadReadsThem()
		{
			var topics = new List<TopicView> { new TopicView("t1"), new TopicView("t2"), new TopicView("t3") };
			string filePath = "testTopicsView.txt";
			
			try
			{
				// Save and verify file contents
				TopicView.SaveToFile(topics,filePath);
				Assert.True(File.Exists(filePath));

				var lines = File.ReadAllLines(filePath).Select(l => l.TrimEnd('\r', '\n')).ToArray();
				Assert.Equal(new[] { "t1", "t2", "t3" }, lines);

				// Now test Load reads file into collection
				var coll = new List<TopicView>();
				TopicView.Load(coll, filePath);
				Assert.Equal(3, coll.Count);
				Assert.Equal("t1", coll[0].Topic);
				Assert.Equal("t2", coll[1].Topic);
				Assert.Equal("t3", coll[2].Topic);
			}
			finally
			{
				// cleanup
				try { if (File.Exists(filePath)) File.Delete(filePath); } catch { }
			}
		}

		[Fact]
		public void PropertySetters_RaisePropertyChanged()
		{
			var tv = new TopicView("topic");
			int count = 0;
			string last = null!;
			tv.PropertyChanged += (s, e) => { count++; last = e.PropertyName; };

			tv.IsToRemove = true;
			Assert.Equal(1, count);
			Assert.Equal(nameof(TopicView.IsToRemove), last);

			tv.IsChecked = true;
			Assert.Equal(2, count);
			Assert.Equal(nameof(TopicView.IsChecked), last);

			tv.IsVisibleButton = true;
			Assert.Equal(3, count);
			Assert.Equal(nameof(TopicView.IsVisibleButton), last);
		}

		[Fact]
		public void Equals_Compares_ByTopic()
		{
			var a = new TopicView("same");
			var b = new TopicView("same");
			var c = new TopicView("different");

			Assert.True(a.Equals(b));
			Assert.True(b.Equals(a));
			Assert.False(a.Equals(c));
			Assert.False(a.Equals(null));
			Assert.False(a.Equals(new object()));
		}
	}
}
