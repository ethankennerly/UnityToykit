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
internal class TestDeck
{
	[Test]
	public void Progress()
	{
		List<int> cards = new List<int>{0, 1, 2, 3};
		Assert.AreEqual(2, Deck.Progress(cards, 0.5f));
		Assert.AreEqual(1, Deck.Progress(cards, 0.5f));
		Assert.AreEqual(3, Deck.Progress(cards, 0.5f));
		Assert.AreEqual(0, Deck.Progress(cards, 1.0f));
	}
}
