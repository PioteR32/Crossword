using Crossword.src;
using Crossword.Views;

namespace Crossword.Pages;

public partial class CrosswordPage : ContentPage
{
	public CrosswordPage(src.Crossword crossword)
	{
		InitializeComponent();
		BindingContext = new CrosswordPageView(crossword);
	}
}