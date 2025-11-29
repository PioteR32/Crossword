using Crossword.src;
namespace CrosswordTests
{
	// Tests for CrosswordGenerator
	public partial class CrosswordClassTests
	{
		static void Main(string[] args)
		{
		}
		public static IEnumerable<object[]> CrosswordThrowExceptionsData =>
			new List<object[]>
			{
				// Invalid because no not enough excercies
			new object[]
			{
			new List<Excercie> { new Excercie("What is 2+2?", "four","Math") },
			new List<string> {"Math"},
			"pass123",
			"MyCrossword"
			},
			// Invalid because bad excercie
			new object[]
			{
			new List<Excercie> { new Excercie("What is 2+2?", "four") },
			new List<string> (),
			"a",
			"MyCrossword"
			},
			new object[]
			{
			new List<Excercie> { new Excercie("What is 2+2?", "four"),new Excercie("What is 2+2?", "four") },
			new List<string> (),
			"oo",
			"MyCrossword"
			},
			// Invalid because no password
			new object[]
			{
			new List<Excercie> { new Excercie("What is 2+2?", "four") },
			new List<string> (),
			"",
			"MyCrossword"
			},
			new object[]
			{
			new List<Excercie> { new Excercie("What is 2+2?", "four") },
			new List<string> (),
			null,
			"MyCrossword"
			},
			// Invalid because no name
			new object[]
			{
			new List<Excercie> { new Excercie("What is 2+2?", "four") },
			new List<string> (),
			"password",
			""
			},
			new object[]
			{
			new List<Excercie> { new Excercie("What is 2+2?", "four") },
			new List<string> (),
			"password",
			null
			},
			
		};
		[Theory]
		[MemberData(nameof(CrosswordThrowExceptionsData))]

		public void CrosswordGeneratorExceptionTest(List<Excercie> excercies, List<string> topics, string password, string name)
		{
			Assert.Throws<Exception>(() => Crossword.src.Crossword.GenerateCrossword(excercies, topics, password, name));
		}
		public static IEnumerable<object[]> CrosswordTestData =>
			new List<object[]>
			{
				// Invalid because no not enough excercies
			new object[]
			{
			new List<Excercie> { new Excercie("What is 2+2?", "four","Math"),
				new Excercie("What is 3+1?", "four","Math"), 
				new Excercie("What is 4+0?", "four","Math"), 
				new Excercie("What is 0+4?", "four","Math"), 
			},
			new List<string> {"Math"},
			"four",
			"MyCrossword",
			},
			};
		[Theory]
		[MemberData(nameof(CrosswordTestData))]

		public void CrosswordGeneratorTest(List<Excercie> excercies, List<string> topics, string password, string name)
		{
			Crossword.src.Crossword crossword = Crossword.src.Crossword.GenerateCrossword(excercies, topics, password, name);
			bool testBool = true;
			int index = 0;
			
			foreach (var excercie in crossword.Excercies.OrderBy(e => e.NumberOfQuestion))
			{
				index++;
				if (excercie.NumberOfQuestion != index)
				{
					testBool = false;
					break;
				}
				
			}

			Assert.True(testBool);
		}
		[Theory]
		[MemberData(nameof(CrosswordTestData))]
		public void GetPositionsInGridTest(List<Excercie> excercies, List<string> topics, string password, string name)
		{
			int[] ints = new int[] { 0, 1, 2, 3};
			Crossword.src.Crossword crossword = Crossword.src.Crossword.GenerateCrossword(excercies, topics, password, name);
			var positions = crossword.GetStartPositionsInGrid().OrderBy(c => c).ToArray();
			Assert.True(positions.SequenceEqual(ints));
		}
	}

}
