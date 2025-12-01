using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using Crossword.src;
using Crossword.Views;
using Xunit;

namespace Crossword.Tests
{
	public class ExcercieViewTests
	{
		// Helper to set the private static readonly backing field for FilePath
		

		[Fact]
		public void TopicsConverter_ReturnsStrings()
		{
			var input = new List<TopicView> { new TopicView("T1"), new TopicView("T2") };
			var output = ExcercieView.TopicsConverter(input).ToList();
			Assert.Equal(new[] { "T1", "T2" }, output);
		}

		[Fact]
		public void AddTopics_AddsOnlyCheckedAndUnique()
		{
			var ev = new ExcercieView("q", "ans");
			var topics = new List<TopicView>
			{
				new TopicView("A"){ IsChecked = true },
				new TopicView("A"){ IsChecked = true }, // duplicate by Topic
                new TopicView("B"){ IsChecked = false },
				new TopicView("C"){ IsChecked = true }
			};
			ev.AddTopics(topics);
			var names = ev.TopicsView.Select(t => t.Topic).ToList();
			Assert.Contains("A", names);
			Assert.Contains("C", names);
			Assert.DoesNotContain("B", names);
			Assert.Equal(2, names.Count);
		}

		[Fact]
		public void AddToDeleteCollection_TogglesAndManagesList()
		{
			var tv = new TopicView("X");
			var ev = new ExcercieView("q", "ans");
			// ensure TopicsToDelete initially empty
			ev.AddToDeleteCollection(tv);
			Assert.Contains(ev.TopicsToDelete, t => t.Topic == "X");
			// toggling again removes
			ev.AddToDeleteCollection(tv);
			Assert.DoesNotContain(ev.TopicsToDelete, t => t.Topic == "X");
		}


		[Fact]
		public void DeleteTopicChanges_ReplacesTopicsViewWithCopy()
		{
			var ev = new ExcercieView("q", "ans");
			ev.CopyTopics = new ObservableCollection<TopicView> { new TopicView("c1"), new TopicView("c2") };
			ev.DeleteTopicChanges();
			Assert.Equal(2, ev.TopicsView.Count);
			Assert.Contains(ev.TopicsView, t => t.Topic == "c1");
		}

		[Fact]
		public void LoadFromString_ParsesStringIntoCollection()
		{
			var src = "q1;a1;t1,t2\nq2;a2;\n\n";
			var coll = new List<ExcercieView>();
			ExcercieView.LoadFromString(src, coll);
			Assert.Equal(2, coll.Count);
			Assert.Equal("q1", coll[0].question);
			Assert.Equal("a1", coll[0].correctAnswer);
			Assert.Equal("q2", coll[1].question);
		}
	}
}