using Crossword.src;

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
				typeof(List<Letter>),
				typeof(Keyboard),
				new List<Letter>());
	public List<Letter> Keys
	{
		get => (List<Letter>)GetValue(KeysProperty);
		set => SetValue(KeysProperty, value);
	}


}