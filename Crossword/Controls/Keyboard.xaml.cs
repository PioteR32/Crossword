using Crossword.src;
using System.Collections.ObjectModel;
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
	public static readonly BindableProperty KeysProperty =
			BindableProperty.Create(
				nameof(Keys),
				typeof(IEnumerable<LetterView>),
				typeof(Keyboard),
				default(IEnumerable<LetterView>));

	public static readonly BindableProperty LetterCommandProperty =
			BindableProperty.Create(
				nameof(LetterCommand),
				typeof(ICommand),
				typeof(Keyboard),
				null);

	public IEnumerable<LetterView> Keys
	{
		get => (IEnumerable<LetterView>)GetValue(KeysProperty);
		set => SetValue(KeysProperty, value);
	}
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