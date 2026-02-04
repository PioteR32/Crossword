
using Crossword.src;
using Crossword.Views;

namespace Crossword
{
    public partial class MainPage : ContentPage
    {

        public MainPage(MainPageView mainPageView)
        {
            Task.Run(() =>
            {
                ExcercieView.Load(AppData.Excercies, ExcercieView.FilePath);
                TopicView.Load(AppData.Topics, TopicView.FilePath);
                CrosswordView.Load(AppData.Crosswords, CrosswordView.FilePath);
            });

			InitializeComponent();
            mainPageView.navigation = Navigation;
            BindingContext = mainPageView;
		}
		
    }
}
