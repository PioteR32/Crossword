namespace Crossword.Pages;

public partial class AddCrosswordKeysPage : ContentPage
{
	public AddCrosswordKeysPage()
	{
		InitializeComponent();
	}
	protected override void  OnAppearing()
	{
		base.OnAppearing();
		//BindingContext = new Crossword.Views.AddCrosswordKeyPageView();
	}
}