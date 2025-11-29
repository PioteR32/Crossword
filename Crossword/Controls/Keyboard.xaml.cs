using Crossword.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;

namespace Crossword.Controls;

public partial class Keyboard : ContentView
{
	public Keyboard()
	{
#if WINDOWS || ANDROID || IOS || MACCATALYST
		InitializeComponent();
#endif
	}
	public ObservableCollection<ObservableCollection<LetterView>> letterViews = new ObservableCollection<ObservableCollection<LetterView>>();
	public ObservableCollection<ObservableCollection<LetterView>> LetterViews
	{
		get => letterViews;
		set
		{
			letterViews = value;
			OnPropertyChanged(nameof(letterViews));
		}
	}
	public static readonly BindableProperty KeysListProperty =
			BindableProperty.Create(
				nameof(KeysList),
				typeof(ObservableCollection<LetterView>),
				typeof(Keyboard),
				default(ObservableCollection<LetterView>),
				propertyChanged: OnKeysChanged

				);
	public ObservableCollection<LetterView> KeysList
	{
		get => (ObservableCollection<LetterView>)GetValue(KeysListProperty);
		set => SetValue(KeysListProperty, value);
	}

	static void OnKeysChanged(BindableObject obj, object oldValue, object newValue)
	{
		if (newValue is not ObservableCollection<LetterView> letterViewList || obj is not Keyboard keyboard) return;
		SortedDictionary<int, ObservableCollection<LetterView>> keys = new SortedDictionary<int, ObservableCollection<LetterView>>();
		
		foreach ( var letterView in letterViewList)
		{
			if(keys.TryGetValue(letterView.Y,out ObservableCollection< LetterView > tmp))
			{
				//if(keys[letterView.Y].Count == 0)
				//	keys[letterView.Y].Add(letterView);
				keys[letterView.Y].Add(letterView);
			}
			else
			{
				keys.Add(letterView.Y, new ObservableCollection<LetterView>());
				keys[letterView.Y].Add(letterView);
			}	
		}
		int beforeKey = -1;
		foreach( var key in keys)
		{
			if (key.Key == beforeKey + 1)
			{
				keyboard.LetterViews.Add(key.Value);
				keyboard.LetterViews[key.Key] = new ObservableCollection<LetterView>(key.Value.OrderBy(x => x.X));
				letterViewList.CollectionChanged += (s, e) =>
				{
					switch (e.Action)
					{
						case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
							foreach (LetterView item in e.NewItems)
								if(item.Y == key.Key)
									keyboard.LetterViews[key.Key].Add(item);
							break;

						case NotifyCollectionChangedAction.Remove:
							foreach (LetterView item in e.NewItems)
								if (item.Y == key.Key)
									keyboard.LetterViews[key.Key].Remove(item);
							break;

						case NotifyCollectionChangedAction.Replace:
							if(e.NewItems[0] is LetterView lv && lv.Y == key.Key)
								keyboard.LetterViews[key.Key][e.OldStartingIndex] = lv;
							break;

						case NotifyCollectionChangedAction.Reset:
							keyboard.LetterViews[key.Key].Clear();
							break;
					}
				};
			}
			else
			{
				keyboard.LetterViews.Clear();
				return;
			}
			beforeKey++;
		}
		keyboard.LetterViews = keyboard.LetterViews;
	}
	public static readonly BindableProperty LetterCommandProperty =
			BindableProperty.Create(
				nameof(LetterCommand),
				typeof(ICommand),
				typeof(Keyboard),
				null);

	
	public ICommand LetterCommand
	{
		get => GetValue(LetterCommandProperty) as ICommand;
		set => SetValue(LetterCommandProperty, value);
	}

	public static readonly BindableProperty GridColumnsProperty =
			BindableProperty.Create(
				nameof(GridColumns),
				typeof(int),
				typeof(Keyboard),
				0);

	public int GridColumns
	{
		get => (int)GetValue(GridColumnsProperty);
		set => SetValue(GridColumnsProperty, value);
	}
	
	public static readonly BindableProperty GridRowsProperty =
			BindableProperty.Create(
				nameof(GridRows),
				typeof(int),
				typeof(Keyboard),
				0);

	public int GridRows
	{
		get => (int)GetValue(GridRowsProperty);
		set => SetValue(GridRowsProperty, value);
	}

	//public object LetterCommandParameter
	//{
	//	get => GetValue(LetterCommandParameterProperty);
	//	set => SetValue(LetterCommandParameterProperty, value);
	//}
}