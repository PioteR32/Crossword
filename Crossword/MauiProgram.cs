using Crossword.src;
using Crossword.Views;
using Microsoft.Extensions.Logging;
namespace Crossword
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
			ExcercieView.Load(AppData.Excercies, ExcercieView.FilePath);
			TopicView.Load(AppData.Topics, TopicView.FilePath);
			CrosswordView.Load(AppData.Crosswords, CrosswordView.FilePath);

			var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddTransient<MainPageView>();
            //builder.Services.AddTransient<AddTopicPageView>();
            //builder.Services.AddTransient<CrosswordPageView>();

            return builder.Build();
        }
    }
}
