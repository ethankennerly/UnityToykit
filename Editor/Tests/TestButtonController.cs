using System.Collections.Generic;
using NUnit.Framework;

namespace Finegamedesign.Utils
{
	[TestFixture]
	internal sealed class TestButtonController
	{
		[Test]
		public void Update()
		{
			ButtonController controller = new ButtonController();
			Assert.AreEqual(null, controller.downName);
			Assert.AreEqual(false, controller.isAnyNow);
			controller.Update();
			Assert.AreEqual(false, controller.isAnyNow);
			controller.Down("newGame");
			Assert.AreEqual(null, controller.downName);
			Assert.AreEqual(false, controller.isAnyNow);
			controller.Update();
			Assert.AreEqual(true, controller.isAnyNow);
			Assert.AreEqual("newGame", controller.downName);
			controller.Update();
			Assert.AreEqual(false, controller.isAnyNow);
			Assert.AreEqual(null, controller.downName);
		}
	}
}
