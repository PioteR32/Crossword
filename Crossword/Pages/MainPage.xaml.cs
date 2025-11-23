
namespace Crossword
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }
		private async Task Button_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new Pages.CrosswordPage());
		}

		private async Task Button_Clicked_1(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new Pages.AddCrosswordKeysPage());
		}
    }
}
