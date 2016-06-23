using UnityEngine;
// using UnityEngine.UI;
using NUnit.Framework;

namespace /*<com>*/Finegamedesign.Utils
{
	[TestFixture]
	public class TestTextView
	{
		// Disambiguate from NUnit.Framework.Text
		[Test]
		public void SetTextAndGetText()
		{
			GameObject node = new GameObject();
			SceneNodeView.SetName(node, "TextOwner");
			node.AddComponent<UnityEngine.UI.Text>();
			TextView.SetText(node, "Hello world");
			Assert.AreEqual("Hello world",
				TextView.GetText(node));
			SceneNodeView.SetVisible(node, false);
			Object.DestroyImmediate(node);
		}

		[Test]
		public void SetTextAndGetTextOnTextMesh()
		{
			GameObject node = new GameObject();
			SceneNodeView.SetName(node, "TextMeshOwner");
			node.AddComponent<TextMesh>();
			TextView.SetText(node, "Hello world");
			Assert.AreEqual("Hello world",
				TextView.GetText(node));
			SceneNodeView.SetVisible(node, false);
			Object.DestroyImmediate(node);
		}
	}
}
