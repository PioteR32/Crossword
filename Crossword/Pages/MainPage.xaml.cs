
using Crossword.Views;

namespace Crossword
{
    public partial class MainPage : ContentPage
    {

        public MainPage(MainPageView mainPageView)
        {
            InitializeComponent();
            mainPageView.navigation = Navigation;
            BindingContext = mainPageView;
		}
		
    }
}
