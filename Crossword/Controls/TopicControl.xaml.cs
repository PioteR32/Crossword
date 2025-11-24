using System.Collections.ObjectModel;
using System.Windows.Input;
using Crossword.src;
namespace Crossword.Controls;

public partial class TopicControl : ContentView
{
	public TopicControl()
	{
		InitializeComponent();
	}


	// Add this BindableProperty for Topic
	public static readonly BindableProperty TopicProperty =
		BindableProperty.Create(nameof(Topic), typeof(string), typeof(TopicControl), default(string));

	public string Topic
	{
		get => (string)GetValue(TopicProperty);
		set => SetValue(TopicProperty, value);
	}

	public static readonly BindableProperty IsVisibleRemoveButtonProperty =
	 BindableProperty.Create(
		 nameof(IsVisibleRemoveButton),
		 typeof(bool),
		 typeof(TopicControl),
		 false);

	public bool IsVisibleRemoveButton
	{
		get => (bool)GetValue(IsVisibleRemoveButtonProperty);
		set => SetValue(IsVisibleRemoveButtonProperty, value);
	}

	public static readonly BindableProperty IsToRemoveProperty =
	BindableProperty.Create(
		nameof(IsToRemove),
		typeof(bool),
		typeof(TopicControl),
		false);

	public bool IsToRemove
	{
		get => (bool)GetValue(IsToRemoveProperty);
		set => SetValue(IsToRemoveProperty, value);
	}


	public static readonly BindableProperty TopicCommandProperty =
	BindableProperty.Create(
		nameof(TopicCommand),
		typeof(ICommand),
		typeof(TopicControl));

	public ICommand TopicCommand
	{
		get => (ICommand)GetValue(TopicCommandProperty);
		set => SetValue(TopicCommandProperty, value);
	}

}