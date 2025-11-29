using Crossword.Pages;

namespace Crossword
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
			Routing.RegisterRoute(nameof(AddCrosswordKeysPage), typeof(AddCrosswordKeysPage));
			Routing.RegisterRoute(nameof(CrosswordPage), typeof(CrosswordPage));
		}
    }
}
