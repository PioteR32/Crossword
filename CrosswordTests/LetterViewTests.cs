using System.ComponentModel;
using Crossword.Views;
using Xunit;

namespace Crossword.Tests
{
	public class LetterViewTests
	{
		[Fact]
		public void X_Y_PropertyChanged_Raised()
		{
			var lv = new LetterView("A", 2, 3);
			int propertyChangedCount = 0;
			string lastProp = null;
			lv.PropertyChanged += (s, e) => { propertyChangedCount++; lastProp = e.PropertyName; };

			// change X
			lv.X = 10;
			Assert.Equal(1, propertyChangedCount);
			Assert.Equal(nameof(lv.X), lastProp);

			// change Y
			lv.Y = 11;
			Assert.Equal(2, propertyChangedCount);
			Assert.Equal(nameof(lv.Y), lastProp);
		}

		[Fact]
		public void Constructor_SetsCoordinates()
		{
			var lv = new LetterView("B", 5, 6);
			Assert.Equal(5, lv.Y);
			Assert.Equal(6, lv.X);
		}
	}
}