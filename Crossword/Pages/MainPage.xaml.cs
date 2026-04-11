
using Crossword.src;
using Crossword.Views;

namespace Crossword
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
			
			InitializeComponent();
			MainPageView  mpv = BindingContext as MainPageView;
            mpv.navigation = Navigation;
		}
		
    }
}
