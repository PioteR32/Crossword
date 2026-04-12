using CommunityToolkit.Mvvm.Input;
using Crossword.src;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Previewer;
using QuestPDF.Infrastructure;

namespace Crossword.Views
{
	internal partial class CrosswordPageView : BindableObject
	{
		public ObservableCollection<ExcercieView> Excercies { get; private set; }
		public ObservableCollection<LetterView> Letters { get; private set; }
		ExcercieView selectedExcercie = null;
		Crossword.src.Crossword crossword;
		public int TotalExcercies
		{
			get => Excercies.Count;
		}

		public int PasswordMainLettersIndex { get; private set; } = 4;
		public int NumberOfGridColumns
		{
			get; private set;
		}
		public ExcercieView SelectedExcercie
		{
			get => selectedExcercie;
			set
			{
				selectedExcercie = value;
				OnPropertyChanged();
			}
		}
		void ConstructorMainFnc()
		{
			FocuseButton = new Command<ExcercieView>(
			(ExcercieView excercie) =>
			{
				foreach (ExcercieView exc in Excercies)
				{
					exc.IsChecked = false;
				}
				excercie.IsChecked = true;
				SelectedExcercie = excercie;
			});

			Letters = new ObservableCollection<LetterView> {
				new LetterView("q",0,0),
				new LetterView("w", 0, 1),
				new LetterView("e",0,2),
				new LetterView("r", 0, 3),
				new LetterView("t",0,4),
				new LetterView("y", 0, 5),
				new LetterView("u",0,6),
				new LetterView("i", 0, 7),
				new LetterView("o",0,8),
				new LetterView("p", 0, 9),

				new LetterView("a",1,0),
				new LetterView("s", 1, 1),
				new LetterView("d",1,2),
				new LetterView("f", 1, 3),
				new LetterView("g",1,4),
				new LetterView("h", 1, 5),
				new LetterView("j",1,6),
				new LetterView("k", 1, 7),
				new LetterView("l",1,8),

				new LetterView("z", 2, 0),
				new LetterView("x",2,1),
				new LetterView("c", 2, 2),
				new LetterView("v",2,3),
				new LetterView("b", 2, 4),
				new LetterView("n",2,5),
				new LetterView("m", 2, 6),
				new LetterView("<--", 2, 7),
			};

			Clickced = new Command<LetterView>(AddLetter);
			NumberOfGridColumns = 20;
		}
		public CrosswordPageView()
		{
			ConstructorMainFnc();
			Excercies = new ObservableCollection<ExcercieView> { new ExcercieView("test", "atest", 0), new ExcercieView("test", "atest", 2) };
		}
		public CrosswordPageView(Crossword.src.Crossword crossword)
		{
			ConstructorMainFnc();
			ObservableCollection<ExcercieView> tmpExcercies = new();
			int[] startPositions = crossword.GetStartPositionsInGrid();
			for (int i = 0; i < crossword.Excercies.Length; i++)
			{
				Excercie exc = crossword.Excercies[i];
				tmpExcercies.Add(new ExcercieView(exc.question, exc.correctAnswer, startPositions[i], i + 1));
			}
			Excercies = tmpExcercies;
			PasswordMainLettersIndex = crossword.MainPasswordIndex;
			this.crossword = crossword;
		}
		public ICommand Clickced { get; private set; }
		public ICommand FocuseButton { get; private set; }
		public ICommand CheckAnswers { get; private set; }
		int goodAnswers = 0;
		public int GoodAnswers
		{
			get => goodAnswers; set { goodAnswers = value; OnPropertyChanged(); }
		}
		void CheckAllAnswers()
		{
			foreach (ExcercieView exc in Excercies)
			{
				string ans = "";
				foreach (PasswordChar pc in exc.answer)
				{
					ans += pc.Character;
				}

				if (ans == exc.correctAnswer)
					GoodAnswers++;
			}
		}
		void AddLetter(LetterView letter)
		{
			ExcercieView excercie = SelectedExcercie;

			if (excercie != null)
			{
				if (excercie.IsChecked == true)
				{
					if (letter.Name == "<--")
					{
						if (excercie.ActualPositionInTable - excercie.StartPositionInGrid != 0)
							excercie.answer[excercie.ActualPositionInTable - 1].Character = '\0';
					}
					else if (excercie.ActualPositionInTable < excercie.GridColumnCount)
					{
						excercie.answer[excercie.ActualPositionInTable].Character = letter.Character;
					}
				}

			}
		}
		[RelayCommand]
		public async Task GeneratePDFCrossword()
		{
			try
			{
				QuestPDF.Settings.License = LicenseType.Community;
				Document.Create(container =>
				{
					container.Page(page =>
					{
						page.Size(PageSizes.A4);
						page.Header().Text(crossword.Name).AlignCenter().FontSize(10).Bold().Underline();

						page.Content().PaddingVertical(10).Column(col =>
						{
							col.Item().AlignCenter().Table(table =>
							{
								table.ColumnsDefinition(columns =>
								{
									for (int i = 0; i < NumberOfGridColumns; i++)
									{
										columns.ConstantColumn(20);
									}
								});

								foreach (ExcercieView exc in Excercies)
								{
									// 1. Definiujemy 3 kolumny

									string ans = "";
									for (int i = 0; i < NumberOfGridColumns; i++)
									{
										static QuestPDF.Infrastructure.IContainer ContentStyle(QuestPDF.Infrastructure.IContainer container, char ch)
										{
											// Musimy PRZYPISAĆ wynik operacji do kontenera lub od razu go zwrócić
											if (ch == '\0')
											{
												return container
													.Border(2)
													.BorderColor(QuestPDF.Helpers.Colors.Black);
											}
											else if (ch == '@')
											{
												return container
													.PaddingHorizontal(5);
											}
											else if (ch == '`')
											{
												return container;
											}
											else
												return container
													.Border(2)
													.BorderColor(QuestPDF.Helpers.Colors.Black)
													.Background(QuestPDF.Helpers.Colors.Red.Medium);
										}
										if (i > exc.StartPositionInGrid && i < exc.answer.Length + 1)
										{
											PasswordChar pc = exc.answer[i - 1 - exc.StartPositionInGrid];
											if (i == PasswordMainLettersIndex + 1)
												table.Cell().Element(c => ContentStyle(c, '#')).Text(" ");
											else if (pc.Character == '\0')
												table.Cell().Element(c => ContentStyle(c, pc.Character)).Text(" ");
											else
												table.Cell().Element(c => ContentStyle(c, pc.Character)).Text(pc.Character);
										}
										else if (exc.StartPositionInGrid == i)
											table.Cell().Element(c => ContentStyle(c, '@')).Text(exc.NumberOfQuestion + ".");
										else
											table.Cell().Element(c => ContentStyle(c, '`')).Text("");
										// Przekazujemy kontener ORAZ obiekt klasy pc (np. PasswordChar)

										Console.WriteLine(ans);
									}
								}


								foreach (ExcercieView exc in Excercies)
								{
									col.Item().PaddingHorizontal(5).AlignLeft().Text(exc.NumberOfQuestion + ". " + exc.question);
								}

								// 3. Wypełnianie danymi z pętli
								//foreach (var item in Excercies)
								//{
								//	table.Cell().Element(ContentStyle).Text(item.NumberOfQuestion);


								//	static QuestPDF.Infrastructure.IContainer ContentStyle(QuestPDF.Infrastructure.IContainer container) =>
								//		container.PaddingVertical(5)
								//				 .Border(2)
								//				 .BorderColor(QuestPDF.Helpers.Colors.Black);
								//}
							});
						});

					});
				}).GeneratePdf(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), crossword.Name + ".pdf"));
			}
			catch (Exception ex)
			{
				await Shell.Current.DisplayAlert("Generowanie Pdf", "Wystąpił błąd: " + ex.Message, "OK");
				return;
			}
			await Shell.Current.DisplayAlert("Generowanie Pdf", "Zakończono sukcesem", "OK");
		}
	}
}
