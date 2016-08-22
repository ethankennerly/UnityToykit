using NUnit.Framework;

namespace Finegamedesign.Utils
{
	[TestFixture]
	internal sealed class TestStringUtil
	{
		[Test]
		public void ParseIndex()
		{
			Assert.AreEqual(-1, StringUtil.ParseIndex(""));
			Assert.AreEqual(-1, StringUtil.ParseIndex("a"));
			Assert.AreEqual(-1, StringUtil.ParseIndex("b_"));
			Assert.AreEqual(2, StringUtil.ParseIndex("c_2"));
			Assert.AreEqual(3, StringUtil.ParseIndex("d_3_0"));
			Assert.AreEqual(-1, StringUtil.ParseIndex("_e_3_1"));
			Assert.AreEqual(4, StringUtil.ParseIndex("_4_2"));
		}
	}
}
