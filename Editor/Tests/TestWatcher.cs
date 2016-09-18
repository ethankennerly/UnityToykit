using NUnit.Framework;

namespace Finegamedesign.Utils
{
	public sealed class TestWatcher
	{
		[Test]
		public void UpdateIsChangeTo()
		{
			bool isInMenu = true;
			var isInMenuWatcher = Watcher<bool>.Create(isInMenu);
			isInMenuWatcher.Update(isInMenu);
			Assert.AreEqual(false, isInMenuWatcher.IsChangeTo(false));
			Assert.AreEqual(false, isInMenuWatcher.IsChange());
			isInMenu = false;
			Assert.AreEqual(false, isInMenuWatcher.IsChangeTo(false));
			Assert.AreEqual(false, isInMenuWatcher.IsChange());
			isInMenuWatcher.Update(isInMenu);
			Assert.AreEqual(true, isInMenuWatcher.IsChangeTo(false));
			Assert.AreEqual(true, isInMenuWatcher.IsChange());
			Assert.AreEqual(false, isInMenuWatcher.IsChangeTo(true));
			isInMenuWatcher.Setup(isInMenu);
			Assert.AreEqual(false, isInMenuWatcher.IsChangeTo(false));
			Assert.AreEqual(false, isInMenuWatcher.IsChange());
			Assert.AreEqual(false, isInMenuWatcher.IsChangeTo(true));
		}
	}
}
