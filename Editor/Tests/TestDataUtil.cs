using NUnit.Framework/*<Test>*/;
using System.Collections.Generic/*<List>*/;

namespace Finegamedesign.Utils
{
	public sealed class TestDataUtil
	{
		[Test]
		public void JoinListOfInt()
		{
			List<int> numbers = new List<int>();
			numbers.Add(3);
			numbers.Add(5);
			numbers.Add(4);
			Assert.AreEqual("3, 5, 4", DataUtil.Join(numbers, ", "));
		}
	}
}
