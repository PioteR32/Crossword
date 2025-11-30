using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.src
{
	public class Crossword
	{
		public Excercie[] Excercies { get; set; }
		public int MainPasswordIndex { get; set; } = 0;
		public string Password { get;  set; } = "";
		public string Name { get;  set; } = "";
		public Crossword(string name)
		{
			Name = name;
		}
		public Crossword(Crossword crossword)
		{
			this.Name = crossword.Name;
			this.Password = crossword.Password;
			this.MainPasswordIndex = crossword.MainPasswordIndex;
			this.Excercies = crossword.Excercies;
		}
		public static Crossword GenerateCrossword(List<Excercie> excercies
			, List<string> crosswordTopics
			, string password
			,string name)
		{
			if (password is null)
				throw new Exception("Password cannot be empty");
			if (name == null)
				throw new Exception("Name cannot be empty");
			if (excercies == null || excercies.Count == 0)
				throw new Exception("Excercies cannot be empty");
			if (name.Length == 0)
				throw new Exception("Name cannot be empty");
			if (password.Length == 0)
				throw new Exception("Password cannot be empty");
			Crossword crossword = new Crossword(name);
			crossword.MainPasswordIndex = 0;
			crossword.Password = password;

			List<Excercie> selectedExcercies;
			if (crosswordTopics.Count == 0)
				selectedExcercies = excercies;
			else
				selectedExcercies = excercies.Where(ex => ex.Topics.Any(t => crosswordTopics.Contains(t))).ToList();
			
			if(selectedExcercies.Count < password.Length)
				throw new Exception("Not enough excercies to generate crossword with this password and topics");

			Dictionary<char, List<Excercie>> excerciesByLetter = new Dictionary<char, List<Excercie>>();
			crossword.Excercies = new Excercie[password.Length];

			var numberOfTypeLettterInPassword = password.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());

			foreach(var kvp in numberOfTypeLettterInPassword)
			{
				List<Excercie> excerciesWithLetter = selectedExcercies.Where(ex => 
				ex.correctAnswer.Contains(kvp.Key)).ToList();

				if (excerciesWithLetter.Count < kvp.Value)
					throw new Exception($"Not enough excercies with letter {kvp.Key} to generate crossword with this password and topics");
				excerciesByLetter.Add(kvp.Key, excerciesWithLetter);
			}
			
			excerciesByLetter = excerciesByLetter.OrderBy(kvp => kvp.Value.Count / numberOfTypeLettterInPassword[kvp.Key]).ToDictionary();

			foreach(var kvp in excerciesByLetter)
			{
				char letter = kvp.Key;
				List<Excercie> excerciesWithLetter = kvp.Value;
				Excercie selectedExcercie = null;
				for(int i = 0;i < numberOfTypeLettterInPassword[kvp.Key]; i++)
				{
					foreach (Excercie ex in excerciesWithLetter)
					{
						if (!crossword.Excercies.Contains(ex))
						{
							selectedExcercie = ex;
							break;
						}
					}
					if (selectedExcercie == null)
					{
						throw new Exception($"Not enough excercies with letter {kvp.Key} to generate crossword with this password and topics");
					}

					int positionInPassword = 0;
					int sameLetters = 0;
					for(int itemIndex = 0; itemIndex < selectedExcercie.correctAnswer.Length; itemIndex++)
					{
						char item = selectedExcercie.correctAnswer[itemIndex];
						
						if(item == letter)
							sameLetters++;
						if(item == letter && sameLetters == i +1)
						{
							positionInPassword = itemIndex;
							break;
						}
					}
					selectedExcercie.NumberOfQuestion = positionInPassword + 1;
					crossword.Excercies[positionInPassword] = selectedExcercie;
				}
				

			}

			for(int i = 0; i < password.Length;i++)
			{
				int indexOfLetter = crossword.Excercies[i].correctAnswer.IndexOf(password[i]);
				if (crossword.MainPasswordIndex < indexOfLetter )
					crossword.MainPasswordIndex = indexOfLetter;
			}

			return crossword;
		}
		public int[] GetStartPositionsInGrid()
		{
			int[] positions = new int[Excercies.Length];
			for(int i = 0; i < Excercies.Length; i++)
			{
				positions[i] = MainPasswordIndex - Excercies[i].correctAnswer.IndexOf(Password[i]);
			}
			return positions;
		}
		
		public static bool operator ==(Crossword a, Crossword b)
		{
			return a.Excercies.SequenceEqual(b.Excercies)
				&& a.Password == b.Password
				&& a.Name == b.Name
				&& a.MainPasswordIndex == b.MainPasswordIndex;
		}
		public static bool operator !=(Crossword a, Crossword b)
		{
			return !(a.Excercies.SequenceEqual(b.Excercies)
				&& a.Password == b.Password
				&& a.Name == b.Name
				&& a.MainPasswordIndex == b.MainPasswordIndex);
		}
	}

}
