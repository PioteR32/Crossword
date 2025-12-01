
using Crossword.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.src
{
	public static class AppData
	{
		public static List<ExcercieView> Excercies { get; private set; } = new List<ExcercieView>();
		public static List<TopicView> Topics { get; private set; } = new List<TopicView>();
		public static List<CrosswordView> Crosswords { get; private set; } = new List<CrosswordView>();
		
		public static void SaveExcercies(IEnumerable<ExcercieView> excercies)
		{
			Excercies = new List<ExcercieView>(excercies);
			ExcercieView.SaveToFile(excercies,ExcercieView.FilePath);
		}
		
		public static void SaveTopics(IEnumerable<TopicView> excercies)
		{
			Topics = new List<TopicView>(excercies);
            TopicView.SaveToFile(excercies,TopicView.FilePath);
		}
		public static void SaveCrosswords(IEnumerable<CrosswordView> excercies)
		{
			Crosswords = new List<CrosswordView>(excercies);
			CrosswordView.SaveToFile(excercies, CrosswordView.FilePath);
		}
		
	}
}
