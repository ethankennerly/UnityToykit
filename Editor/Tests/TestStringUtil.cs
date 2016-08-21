/**
 * This script must be in an Editor folder.
 * Test case:  2014-01 JimboFBX expects "using NUnit.Framework;"
 * Got "The type or namespace 'NUnit' could not be found."
 * http://answers.unity3d.com/questions/610988/unit-testing-unity-test-tools-v10-namespace-nunit.html
 */
using NUnit.Framework;

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
