using Crossword.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordTests
{
	public class AddTopicPageViewTests
	{
		[Fact]
		public void TopicStringTest()
		{
			AddTopicPageView addTopicPageView = new AddTopicPageView();
			addTopicPageView.Topic = "Science,Math";
			Assert.True(addTopicPageView.Topic == "sciencemath");
		}
		[Fact]
		public void AddTopicCommandTest()
		{
			AddTopicPageView addTopicPageView = new AddTopicPageView();
			addTopicPageView.Topic = "Math";
			addTopicPageView.AddTopicCommand.Execute(addTopicPageView.Topic);
			addTopicPageView.Topic = "Math,";
			addTopicPageView.AddTopicCommand.Execute(addTopicPageView.Topic);
			Assert.True(addTopicPageView.Topics.SequenceEqual(new List<TopicView> { new TopicView("math")}));
		}
	}
}
