
using Crossword.src;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
namespace Crossword.Views
{
	

	public partial class MainPageView : ObservableObject
	{
		public INavigation navigation;
		public ObservableCollection<CrosswordView> Crosswords { get; private set; } = new ObservableCollection<CrosswordView>();
		List<TopicView> topics = new List<TopicView>(AppData.Topics);
		public List<TopicView> Topics 
		{ 
			get => topics;
			private set
			{
				topics = value;
				OnPropertyChanged();
			}
		}
		string nameOfNewCrossword;
		public string NameOfNewCrossword
		{
			get => nameOfNewCrossword;
			set
			{
				nameOfNewCrossword = value;
				OnPropertyChanged();
			}
		}
		string passwordForNewCrossword;
		public string PasswordForNewCrossword
		{
			get => passwordForNewCrossword;
			set
			{
				passwordForNewCrossword = value;
				OnPropertyChanged();
			}
		}
		List<string> GetCheckedTopics()
		{
			List<string> checkedTopics = new List<string>();
			foreach (var topic in Topics)
			{
				if (topic.IsChecked)
					checkedTopics.Add(topic.Topic);
			}
			return checkedTopics;
		}
		List<Excercie> GetExcerciesFromExcercieViews(IEnumerable<ExcercieView> excercieViews)
		{
			List<Excercie> excercies = new List<Excercie>();
			foreach(var excercieView in excercieViews)
			{
				excercies.Add(new Excercie(excercieView.QuestionView, excercieView.CorrectAnswerView, ExcercieView.TopicsConverter(excercieView.Topics).ToArray()));
			}
			return excercies;
		}
		[RelayCommand]
		public async Task GoToAddCrosswordPageKeys()
		{
			await Shell.Current.GoToAsync(nameof(Crossword.Pages.AddCrosswordKeysPage));
		}
		[RelayCommand]
		public async Task GoToCrosswordPage(src.Crossword crossword)
		{
			await navigation.PushAsync(new Crossword.Pages.CrosswordPage(crossword));
		}
		[RelayCommand]
		public async Task AddNewCrossword()
		{
			Crosswords.Add(new CrosswordView(src.Crossword.GenerateCrossword(
					GetExcerciesFromExcercieViews(AppData.Excercies)
					, GetCheckedTopics()
					, PasswordForNewCrossword
					, NameOfNewCrossword)));
		}
	}

}

