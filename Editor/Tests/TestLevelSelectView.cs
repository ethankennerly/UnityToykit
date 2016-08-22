using NUnit.Framework;
using System.Collections.Generic;

namespace Finegamedesign.Utils
{
	public sealed class TestLevelSelectView
	{
		// Not a [Test], because it requires an instance.
		public static LevelSelectView Validate()
		{
			LevelSelectView view = LevelSelectView.GetInstance();
			Assert.AreEqual(false, view == null, 
				"Expected exactly one LevelSelectView.");
			Assert.AreEqual(true, 1 <= DataUtil.Length(view.menus),
				"Expected at least one menu defined.");
			Assert.AreEqual(true, 1 <= DataUtil.Length(view.buttons),
				"Expected at least one button defined.");
			Assert.AreEqual(true, 1 <= DataUtil.Length(view.buttons[0]),
				"Expected at least one button defined.");
			return view;
		}
	}
}
