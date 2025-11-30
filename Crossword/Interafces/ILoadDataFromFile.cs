using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.Interafces
{
	public interface ILoadDataFromFile<T>
	{
		T LoadFromFile(string file);
	}
}
