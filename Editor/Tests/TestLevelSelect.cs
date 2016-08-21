using NUnit.Framework;

namespace Finegamedesign.Utils
{
	[TestFixture]
	public sealed class TestLevelSelectModel
	{
		[Test]
		public void Select()
		{
			LevelSelectModel model = LevelSelectModel();
			model.levelCount = 2939;
			model.menus = new List<int>(){8, 20, 20};
			Assert.AreEqual(false, model.Select(1));
			Assert.AreEqual(true, model.Select(0));
		}
	}
}
