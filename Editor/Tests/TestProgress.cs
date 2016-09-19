using System.Collections.Generic/*<List>*/;
using NUnit.Framework;

namespace Finegamedesign.Utils
{
	public sealed class TestProgress
	{
		[Test]
		public void CreepUp()
		{
			Progress progress = new Progress();
			progress.radius = 0.25f;
			Assert.AreEqual(0.0f, progress.Creep(0.0f / 100));
			Assert.AreEqual(0.0f, progress.Creep(50.0f / 100));
			Assert.AreEqual(0.25f, progress.Creep(100.0f / 100));
			Assert.AreEqual(0.25f + 3.0f / 16.0f, 
				progress.Creep(100.0f / 100));
		}

		[Test]
		public void CreepDown()
		{
			Progress progress = new Progress();
			progress.radius = 0.25f;
			Assert.AreEqual(0.0f, progress.Creep(0 / 100.0f));
			Assert.AreEqual(0.0f, progress.Creep(50 / 100.0f));
			Assert.AreEqual(0.25f, progress.Creep(100 / 100.0f));
			Assert.AreEqual(0.25f - 3.0f / 16.0f, progress.Creep(0 / 100.0f));
			Assert.AreEqual(0.0f, progress.Creep(0 / 100.0f));
			Assert.AreEqual(0.0f, progress.Creep(0 / 100.0f));
			Assert.AreEqual(0.25f, progress.Creep(100 / 100.0f));
		}

		[Test]
		public void CreepCheckpoint()
		{
			Progress progress = new Progress();
			progress.radius = 0.25f;
			progress.SetCheckpointStep(0.125f);
			Assert.AreEqual(0.125f, progress.Creep(100 / 100.0f),
				"checkpoint " + progress.checkpoint.ToString());
			Assert.AreEqual(true, progress.isCheckpoint);
			progress.UpdateCheckpoint();
			progress.Creep(52 / 100.0f);
			Assert.AreEqual(false, progress.isCheckpoint);
			progress.normal = 0.242f;
			Assert.AreEqual(0.25f, progress.Creep(100 / 100.0f));
			Assert.AreEqual(true, progress.isCheckpoint);
			progress.UpdateCheckpoint();
			progress.normal = 0.362f;
			Assert.AreEqual(0.375, progress.Creep(100 / 100.0f));
			Assert.AreEqual(true, progress.isCheckpoint);
			progress.UpdateCheckpoint();
			progress.SetLevelNormal(0);
			Assert.AreEqual(0.125f, progress.Creep(100 / 100.0f),
				"checkpoint " + progress.checkpoint.ToString());
			progress.UpdateCheckpoint();
			progress.SetLevelNormal(740);
			Assert.AreEqual(0.75f, progress.Creep(100 / 100.0f));
		}

		[Test]
		public void PopIndex()
		{
			List<int> cards = new List<int>{10, 11, 12, 13};
			Progress progress = new Progress();
			for (int round = 0; round < 4; round++)
			{
				progress.normal = 0.5f;
				progress.SetupIndexes(DataUtil.Length(cards), 1);
				Assert.AreEqual(2, progress.PopIndex());
				Assert.AreEqual(2, progress.level);
				Assert.AreEqual(4, progress.levelMax);
				Assert.AreEqual(3, progress.PopIndex());
				Assert.AreEqual(3, progress.level);
				Assert.AreEqual(4, progress.levelMax);
				progress.normal = 1.0f;
				Assert.AreEqual(1, progress.PopIndex());
				Assert.AreEqual(1, progress.level);
				Assert.AreEqual(4, progress.levelMax);
			}
		}

		[Test]
		public void GetLevelNormal()
		{
			Progress progress = new Progress();
			progress.levelNormalMax = 1000;
			Assert.AreEqual(0, progress.GetLevelNormal());
			progress.levelMax = 2000;
			progress.normal = 0.5f;
			Assert.AreEqual(500, progress.GetLevelNormal());
			progress.normal = 0.75f;
			Assert.AreEqual(750, progress.GetLevelNormal());
			progress.levelMax = 4000;
			Assert.AreEqual(750, progress.GetLevelNormal());
		}

		[Test]
		public void SetLevelNormal()
		{
			Progress progress = new Progress();
			progress.levelNormalMax = 1000;
			progress.levelUnlocked = 100;
			progress.levelMax = 3000;
			Assert.AreEqual(0.0f, progress.normal);
			Assert.AreEqual(0, progress.GetLevelNormal());
			Assert.AreEqual(100, progress.levelUnlocked);
			progress.SetLevelNormal(500);
			Assert.AreEqual(0.5f, progress.normal);
			Assert.AreEqual(500, progress.GetLevelNormal());
			Assert.AreEqual(1500, progress.levelUnlocked);
			progress.levelMax = 2000;
			progress.SetLevelNormal(500);
			Assert.AreEqual(0.5f, progress.normal);
			Assert.AreEqual(500, progress.GetLevelNormal());
			Assert.AreEqual(1500, progress.levelUnlocked);
			progress.SetLevelNormal(750);
			Assert.AreEqual(0.75f, progress.normal);
			Assert.AreEqual(750, progress.GetLevelNormal());
			Assert.AreEqual(1500, progress.levelUnlocked);
			progress.levelMax = 4000;
			progress.SetLevelNormal(375);
			Assert.AreEqual(0.375f, progress.normal);
			Assert.AreEqual(375, progress.GetLevelNormal());
			Assert.AreEqual(1500, progress.levelUnlocked);
			progress.SetLevelNormal(0);
			Assert.AreEqual(0.0f, progress.normal);
			Assert.AreEqual(0, progress.GetLevelNormal());
			Assert.AreEqual(1500, progress.levelUnlocked);
		}

		[Test]
		public void SetLevelNormalUnlocked()
		{
			Progress progress = new Progress();
			progress.levelNormalMax = 10000;
			progress.levelMax = 3000;
			Assert.AreEqual(0.0f, progress.normal);
			Assert.AreEqual(0, progress.GetLevelNormal());
			progress.SetLevelNormalUnlocked(5000);
			Assert.AreEqual(1500, progress.levelUnlocked);
			progress.SetLevelNormalUnlocked(249);
			Assert.AreEqual(74, progress.levelUnlocked);
		}
	}
}
