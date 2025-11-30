using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.Interafces
{
	public interface IMenageData<T>
	{
		public static abstract string FileName { get; } 
		public static abstract string FilePath { get; }
		public static abstract void SaveToFile(IEnumerable<T> excercies);
		public static abstract void  LoadFromString(string excerciesString, ICollection<T> collection);
		public static abstract void Load(ICollection<T> collection);
	}
}
