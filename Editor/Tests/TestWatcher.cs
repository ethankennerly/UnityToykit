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

		[Test]
		public void CurrentUpdateIsChangeTo()
		{
			var helpState = Watcher<string>.Create("none");
			helpState.Update(helpState.next);
			Assert.AreEqual(false, helpState.IsChangeTo("paused"));
			Assert.AreEqual(false, helpState.IsChange());
			helpState.next = "tutor";
			helpState.Update(helpState.next);
			Assert.AreEqual(false, helpState.IsChangeTo("paused"));
			Assert.AreEqual(true, helpState.IsChange());
			helpState.next = "paused";
			helpState.Update(helpState.next);
			Assert.AreEqual(true, helpState.IsChangeTo("paused"));
			Assert.AreEqual(true, helpState.IsChange());
			helpState.Update(helpState.next);
			Assert.AreEqual(false, helpState.IsChangeTo("paused"));
			Assert.AreEqual(false, helpState.IsChange());
		}

		[Test]
		public void IsEqualToNull()
		{
			var helpState = Watcher<string>.Create(null);
			helpState.Update(helpState.next);
			Assert.AreEqual(false, helpState.IsChange());
			helpState.Update("none");
			Assert.AreEqual(true, helpState.IsChange());
			helpState.Update(null);
			Assert.AreEqual(true, helpState.IsChange());
			helpState.Update(helpState.next);
			Assert.AreEqual(false, helpState.IsChange());
		}
	}
}
