/**
 * This script must be in an Editor folder.
 * Test case:  2014-01 JimboFBX expects "using NUnit.Framework;"
 * Got "The type or namespace 'NUnit' could not be found."
 * http://answers.unity3d.com/questions/610988/unit-testing-unity-test-tools-v10-namespace-nunit.html
 */
using System;  // Exception
using System.Collections.Generic;  // List
using System.Threading;
using UnityEngine;
using NUnit.Framework;

[TestFixture]
internal class TestProgress
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
	public void Pop()
	{
		List<int> cards = new List<int>{10, 11, 12, 13};
		Progress progress = new Progress();
		progress.normal = 0.5f;
		Assert.AreEqual(12, progress.Pop(cards));
		Assert.AreEqual(3, progress.level);
		Assert.AreEqual(4, progress.levelMax);
		Assert.AreEqual(11, progress.Pop(cards));
		Assert.AreEqual(2, progress.level);
		Assert.AreEqual(4, progress.levelMax);
		Assert.AreEqual(13, progress.Pop(cards));
		Assert.AreEqual(4, progress.level);
		Assert.AreEqual(4, progress.levelMax);
		progress.normal = 1.0f;
		Assert.AreEqual(10, progress.Pop(cards));
		Assert.AreEqual(1, progress.level);
		Assert.AreEqual(4, progress.levelMax);
	}

	[Test]
	public void GetLevelNormal()
	{
		Progress progress = new Progress();
		progress.levelNormalMax = 1000;
		Assert.AreEqual(0, progress.GetLevelNormal());
		progress.levelMax = 2000;
		progress.level = 1000;
		Assert.AreEqual(500, progress.GetLevelNormal());
		progress.level = 1500;
		Assert.AreEqual(750, progress.GetLevelNormal());
		progress.levelMax = 4000;
		Assert.AreEqual(375, progress.GetLevelNormal());
	}

	[Test]
	public void SetLevelNormal()
	{
		Progress progress = new Progress();
		progress.levelNormalMax = 1000;
		Assert.AreEqual(0.0f, progress.normal);
		progress.levelMax = 2000;
		progress.SetLevelNormal(500);
		Assert.AreEqual(0.5f, progress.normal);
		progress.SetLevelNormal(750);
		Assert.AreEqual(0.75f, progress.normal);
		progress.levelMax = 4000;
		progress.SetLevelNormal(375);
		Assert.AreEqual(0.375f, progress.normal);
	}
}
