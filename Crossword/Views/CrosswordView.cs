
using Crossword.Interafces;
using Crossword.src;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.Views
{
	public class CrosswordView:Crossword.src.Crossword, INotifyPropertyChanged, IMenageData<CrosswordView>
	{
		public static string FileName { get; } = "Crosswords.txt";
		public static string FilePath { get; } = Path.Combine(FileSystem.AppDataDirectory, FileName);
		public static  void SaveToFile(IEnumerable<CrosswordView> excercies, string filePath)
		{
			try
			{
				StreamWriter streamWriter = new StreamWriter(FilePath);
				foreach (var excercie in excercies)
				{
					streamWriter.WriteLine(excercie.ToString());
				}
				streamWriter.Close();
				MainThread.BeginInvokeOnMainThread(async () =>
				{
					await Shell.Current.DisplayAlert("", "Krzyżowki zapisane", "OK");
				});
			}
			catch(System.IO.IOException ex)
			{
				MainThread.BeginInvokeOnMainThread(async () =>
				{
					await Shell.Current.DisplayAlert("", "Krzyżowka się zapisuje", "OK");
				});
			}
		}

		public static void LoadFromString(string crosswordsString, ICollection<CrosswordView> collection)
		{
			
			if (crosswordsString == null || crosswordsString == "") return;
			string[] crosswordTable = crosswordsString.Split("\n");
			foreach (var crossword in crosswordTable)
			{
				string[] parts = crossword.Split(";");
				if (parts.Length < 4) continue;
				CrosswordView crosswordView = new CrosswordView(parts[0]);
				crosswordView.Password = parts[1];
				crosswordView.MainPasswordIndex = int.Parse(parts[2]);
				string[] excerciesTable = parts[3].Split(",");
				crosswordView.Excercies = new Excercie[excerciesTable.Length];
				foreach (var excercie in excerciesTable)
				{
					string[] excerciePartsTable = excercie.Split("|");
					crosswordView.Excercies[int.Parse(excerciePartsTable[2]) - 1] =new Excercie(excerciePartsTable[0],
						excerciePartsTable[1], int.Parse(excerciePartsTable[2]));
				}
				collection.Add(crosswordView);
			}
		}

		public static void Load(ICollection<CrosswordView> collection, string filePath)
		{
			if (Path.Exists(filePath))
			{
				StreamReader streamReader = new StreamReader(filePath);
				string file = streamReader.ReadToEnd();
				streamReader.Close();
				if (file == null || file == "") return;
				LoadFromString(file, collection);
			}
			else
				File.Create(filePath).Close();
		}


		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}


		public CrosswordView(string name) : base(name)
		{
		}
		public CrosswordView(Crossword.src.Crossword crossword) : base(crossword)
		{
			
		}
		public override string ToString()
		{
			return $"{Name};{Password};{MainPasswordIndex};" +
				$"{string.Join(",", Excercies.Select(e => $"{e.question}|{e.correctAnswer}|{e.NumberOfQuestion}" ))}";
		}
	}
}
