using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;

namespace com.finegamedesign.utils
{
	[TestFixture]
	public class TestSceneNodeView
	{
		[Test]
		public void SetNameAndLocal()
		{
			GameObject parent = new GameObject();
			SceneNodeView.SetName(parent, "parent");
			SceneNodeView.SetLocalX(parent, 10.0f);
			SceneNodeView.SetLocalY(parent, 20.0f);
			Assert.AreEqual("parent", SceneNodeView.GetName(parent));
			Assert.AreEqual(10.0f, SceneNodeView.GetLocalX(parent));
			Assert.AreEqual(20.0f, SceneNodeView.GetLocalY(parent));
			GameObject child = new GameObject();
			SceneNodeView.SetName(child, "child");
			SceneNodeView.SetLocalX(child, -4.0f);
			SceneNodeView.SetLocalY(child, 3.0f);
			SceneNodeView.AddChild(parent, child);
			Assert.AreEqual(-4.0f, SceneNodeView.GetLocalX(child));
			Assert.AreEqual(6.0f, SceneNodeView.GetWorldX(child));
			Assert.AreEqual(3.0f, SceneNodeView.GetLocalY(child));
			Assert.AreEqual(23.0f, SceneNodeView.GetWorldY(child));
		}

		// TODO [Test]
		public void GetChildren()
		{
			GameObject parent = new GameObject();
			GameObject child = new GameObject();
			SceneNodeView.SetName(child, "child");
			SceneNodeView.AddChild(parent, child);
			List<GameObject> children = SceneNodeView.GetChildren(parent);
			Assert.AreEqual(1, DataUtil.Length(children));
			Assert.AreEqual("child", SceneNodeView.GetName(children[0]));
			Assert.AreEqual(child, children[0]);
		}

		[Test]
		public void ToSceneNodeList()
		{
			GameObject parent = new GameObject();
			GameObject child = new GameObject();
			SceneNodeView.SetName(child, "child");
			SceneNodeView.SetLocalX(child, 2.0f);
			SceneNodeView.AddChild(parent, child);
			List<GameObject> children = SceneNodeView.GetChildren(parent);
			List<SceneNode> nodes = SceneNodeView.ToSceneNodeList(children);
			Assert.AreEqual(1, DataUtil.Length(nodes));
			Assert.AreEqual("child", nodes[0].name);
			Assert.AreEqual(2.0f, nodes[0].x);
			Assert.AreEqual(0.0f, nodes[0].y);
		}
	}
}
