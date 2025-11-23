using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.src
{
	public static class AppData
	{
		public static List<ExcercieView> Excercies { get; private set; } = new List<ExcercieView>();
		public static List<TopicView> Topics { get; private set; } = new List<TopicView>();
		public static string AppDataDirectoryPath { get; } = FileSystem.AppDataDirectory;
		public static string ExcerciesFileName { get; } = "Excercies.txt";
		public static string ExcerciesFilePath { get; } = Path.Combine(FileSystem.AppDataDirectory, ExcerciesFileName);
		
		public static void SaveExcercies()
		{
			StreamWriter streamWriter = new StreamWriter(ExcerciesFilePath);
			foreach(var excercie in Excercies)
			{
				streamWriter.WriteLine(excercie.ToString());
			}
			streamWriter.Close();
		}
		public static void SaveChanges(IEnumerable<ExcercieView> excercies)
		{
			Excercies = new List<ExcercieView>(excercies);
			SaveExcercies();
		}
		public static void LoadExcercies()
		{ 
			StreamReader streamReader = new StreamReader(ExcerciesFilePath);
			string file = streamReader.ReadToEnd();
			streamReader.Close();

			string[] excerciesString = file.Split("\n");
			foreach( var excercie in excerciesString)
			{
				string[] excercieStrings = excercie.Split(";");
				Excercies.Add(new ExcercieView(excercieStrings[0], excercieStrings[1]));
			}
		}
	}
}
