using Crossword.src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordTests
{
	public class ExcercieTests
	{
		[Fact]
		public void Constructor_SetsProperties_WithTopics()
		{
			var ex = new Excercie("what", "answer", "t1", "t2");
			Assert.Equal("what", ex.question);
			Assert.Equal("answer", ex.correctAnswer);
			Assert.NotNull(ex.Topics);
			Assert.Equal(new List<string> { "t1", "t2" }, ex.Topics);
			Assert.Equal(1, ex.NumberOfQuestion); // default
		}

		[Fact]
		public void Constructor_WithNumber_SetsNumberAndTopics()
		{
			var ex = new Excercie("q", "a", 5, "topic");
			Assert.Equal(5, ex.NumberOfQuestion);
			Assert.Equal("q", ex.question);
			Assert.Equal("a", ex.correctAnswer);
			Assert.Single(ex.Topics);
			Assert.Equal("topic", ex.Topics[0]);
		}

		[Fact]
		public void Constructor_NumberOnly_SetsNumber_WithoutTopics()
		{
			var ex = new Excercie("q2", "a2", 3);
			Assert.Equal(3, ex.NumberOfQuestion);
			Assert.Equal("q2", ex.question);
			Assert.Equal("a2", ex.correctAnswer);
			// This ctor does not initialize Topics (keeps null)
			Assert.Null(ex.Topics);
		}

		[Fact]
		public void ProtectedConstructor_Sets_CorrectAnswerLength_And_Number()
		{
			// Use reflection to invoke protected ctor: (string question, string correctAnswer, int correctAnswerLength, int numberOfQuestion)
			var type = typeof(Excercie);
			var ctor = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
				.FirstOrDefault(c =>
				{
					var parms = c.GetParameters();
					return parms.Length == 4
						&& parms[0].ParameterType == typeof(string)
						&& parms[1].ParameterType == typeof(string)
						&& parms[2].ParameterType == typeof(int)
						&& parms[3].ParameterType == typeof(int);
				});
			Assert.NotNull(ctor);

			var instance = (Excercie)ctor!.Invoke(new object[] { "pq", "answer", 7, 2 });
			Assert.Equal("pq", instance.question);
			Assert.Equal("answer", instance.correctAnswer);
			Assert.Equal(7, instance.CorrectAnswerLenght);
			Assert.Equal(2, instance.NumberOfQuestion);
		}

		[Fact]
		public void ToString_Includes_CorrectAnswer_Question_And_Topics()
		{
			var ex = new Excercie("q", "ans", "t1", "t2");
			string s = ex.ToString();
			// format is "correctAnswer;question;topic1,topic2"
			Assert.Equal("ans;q;t1,t2", s);
		}

		[Fact]
		public void EqualityOperators_And_Equals_Work_ForEqualObjects()
		{
			var a = new Excercie("q", "ans", "t1", "t2");
			var b = new Excercie("q", "ans", "t1", "t2");
			Assert.True(a == b);
			Assert.False(a != b);
			Assert.True(a.Equals(b));
		}

		[Fact]
		public void EqualityOperators_Consider_TopicsOrder()
		{
			var a = new Excercie("q", "ans", "t1", "t2");
			var b = new Excercie("q", "ans", "t2", "t1");
			// SequenceEqual requires same order -> not equal
			Assert.False(a == b);
			Assert.True(a != b);
			Assert.False(a.Equals(b));
		}

		[Fact]
		public void EqualityOperators_Handle_NullOperands()
		{
			Excercie a = null;
			Excercie b = null;
			Assert.True(a == b);

			var ex = new Excercie("q", "ans", "t");
			Assert.False(ex == null);
			Assert.True(ex != null);
		}

		[Fact]
		public void Equals_ReturnsFalse_ForNonExcercieObject()
		{
			var ex = new Excercie("q", "ans", "t");
			Assert.False(ex.Equals(new object()));
		}
	}
}
