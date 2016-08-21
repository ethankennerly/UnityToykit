using NUnit.Framework;
using System.Collections.Generic;

namespace Finegamedesign.Utils
{
	[TestFixture]
	public sealed class TestLevelSelectController
	{
		[Test]
		public void Select107of109Unlocked()
		{
			LevelSelectController controller = new LevelSelectController();
			TestLevelSelectModel.Configure(controller.model);
			controller.Setup();
			Assert.AreEqual("chapterSelect", controller.model.menuName);
			controller.buttons.Down("chapterSelect_0");
			controller.Update();
			Assert.AreEqual("levelSelect", controller.model.menuName);
			controller.buttons.Down("levelSelect_5");
			controller.Update();
			Assert.AreEqual("wordSelect", controller.model.menuName);
			controller.buttons.Down("wordSelect_7");
			controller.Update();
			Assert.AreEqual("play", controller.model.menuName);
			Assert.AreEqual(107, controller.model.levelSelected);
		}
	}
}
