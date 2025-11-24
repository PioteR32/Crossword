using System.Collections.ObjectModel;
using System.Windows.Input;
using Crossword;
namespace Crossword.Controls;

public partial class TopicView : ContentView
{
	public TopicView()
	{
		InitializeComponent();
	}
    public static readonly BindableProperty TopicViewBindableProperty =
            BindableProperty.Create(
                nameof(TopicViewProperty),
                typeof(src.TopicView),
                typeof(TopicView),
                default(src.TopicView)
                );
    public src.TopicView TopicViewProperty
    {
        get => (src.TopicView)GetValue(TopicViewBindableProperty);
        set => SetValue(TopicViewBindableProperty, value);
    }
    public static readonly BindableProperty IsVisableRemoveButtonBindableProperty =
            BindableProperty.Create(
                nameof(IsVisableRemoveButton),
                typeof(bool),
                typeof(TopicView),
                default(bool)
                );
    public bool IsVisableRemoveButton
    {
        get => (bool)GetValue(IsVisableRemoveButtonBindableProperty);
        set => SetValue(IsVisableRemoveButtonBindableProperty, value);
    }
    public static readonly BindableProperty TopicCommandBindableProperty =
            BindableProperty.Create(
                nameof(TopicCommand),
                typeof(ICommand),
                typeof(TopicView),
                default(Command)
                );
    public ICommand TopicCommand
    {
        get => (ICommand)GetValue(TopicCommandBindableProperty);
        set => SetValue(TopicCommandBindableProperty, value);
    }

}