using NUnit.Framework;
using System.Collections.Generic;

namespace Finegamedesign.Utils
{
	[TestFixture]
	public sealed class TestLevelSelectModel
	{
		[Test]
		public void Select107Unlocked109()
		{
			LevelSelectModel model = new LevelSelectModel();
			model.levelCount = 2939;
			model.levelUnlocked = 109;
			model.menus = new List<int>(){8, 20, 20};
			model.Setup();
			Assert.AreEqual(1, model.levelsPerItem[2]);
			Assert.AreEqual(20, model.levelsPerItem[1]);
			Assert.AreEqual(400, model.levelsPerItem[0]);
			Assert.AreEqual(0, model.menuIndex);
			Assert.AreEqual(false, model.Select(1));
			Assert.AreEqual(0, model.menuIndex);
			Assert.AreEqual(true, model.Select(0));
			Assert.AreEqual(1, model.menuIndex);
			Assert.AreEqual(0, model.context);
			Assert.AreEqual(0, model.requested);
			Assert.AreEqual(false, model.Select(6));
			Assert.AreEqual(1, model.menuIndex);
			Assert.AreEqual(120, model.requested);
			Assert.AreEqual(true, model.Select(5));
			Assert.AreEqual(2, model.menuIndex);
			Assert.AreEqual(100, model.requested);
			Assert.AreEqual(false, model.Select(10));
			Assert.AreEqual(2, model.menuIndex);
			Assert.AreEqual(true, model.Select(7));
			Assert.AreEqual(3, model.menuIndex);
			Assert.AreEqual(107, model.levelSelected);
		}
	}
}
