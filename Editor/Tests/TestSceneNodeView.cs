using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;

namespace /*<com>*/Finegamedesign.Utils
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
			Vector3 position = new Vector3(5.0f, 1.5f);
			SceneNodeView.SetLocal(child, position);
			Assert.AreEqual(5.0f, SceneNodeView.GetLocalX(child));
			Assert.AreEqual(15.0f, SceneNodeView.GetWorldX(child));
			Assert.AreEqual(1.5f, SceneNodeView.GetLocalY(child));
			Assert.AreEqual(21.5f, SceneNodeView.GetWorldY(child));
			Object.DestroyImmediate(parent);
			Object.DestroyImmediate(child);
		}

		[Test]
		public void GetChildren()
		{
			GameObject parent = new GameObject();
			SceneNodeView.SetName(parent, "parent");
			GameObject child = new GameObject();
			SceneNodeView.SetName(child, "child");
			SceneNodeView.AddChild(parent, child);
			List<GameObject> children = SceneNodeView.GetChildren(parent);
			Assert.AreEqual(1, DataUtil.Length(children));
			Assert.AreEqual("child", SceneNodeView.GetName(children[0]));
			Assert.AreEqual(child, children[0]);
			Object.DestroyImmediate(parent);
			Object.DestroyImmediate(child);
		}

		[Test]
		public void GetParent()
		{
			GameObject parent = new GameObject();
			GameObject child = new GameObject();
			SceneNodeView.SetName(parent, "parent");
			SceneNodeView.SetName(child, "child");
			SceneNodeView.AddChild(parent, child);
			Assert.AreEqual("parent", SceneNodeView.GetName(
				SceneNodeView.GetParent(child)));
			Assert.AreEqual(parent,
				SceneNodeView.GetParent(child));
			Object.DestroyImmediate(parent);
			Object.DestroyImmediate(child);
		}

		[Test]
		public void ToSceneNodeList()
		{
			GameObject parent = new GameObject();
			SceneNodeView.SetName(parent, "parent");
			GameObject child = new GameObject();
			SceneNodeView.SetName(child, "child");
			SceneNodeView.SetLocalX(child, 2.0f);
			SceneNodeView.AddChild(parent, child);
			List<GameObject> children = SceneNodeView.GetChildren(parent);
			List<SceneNodeModel> nodes = SceneNodeView.ToSceneNodeList(children);
			Assert.AreEqual(1, DataUtil.Length(nodes));
			Assert.AreEqual("child", nodes[0].name);
			Assert.AreEqual(2.0f, nodes[0].x);
			Assert.AreEqual(0.0f, nodes[0].y);
			Object.DestroyImmediate(parent);
			Object.DestroyImmediate(child);
		}

		[Test]
		public void SetWorldScaleX()
		{
			GameObject parent = new GameObject();
			GameObject child = new GameObject();
			SceneNodeView.AddChild(parent, child);
			SceneNodeView.SetName(parent, "parent");
			SceneNodeView.SetName(child, "child");
			SceneNodeView.SetLocalScaleX(child, 2.0f);
			SceneNodeView.SetLocalScaleX(parent, 3.0f);
			Assert.AreEqual(2.0f,
				SceneNodeView.GetLocalScaleX(child));
			Assert.AreEqual(6.0f,
				SceneNodeView.GetWorldScaleX(child));
			SceneNodeView.SetWorldScaleX(child, 12.0f);
			Assert.AreEqual(12.0f,
				SceneNodeView.GetWorldScaleX(child));
			Assert.AreEqual(4.0f,
				SceneNodeView.GetLocalScaleX(child));
			Assert.AreEqual(1.0f,
				SceneNodeView.GetLocalScaleY(parent));
			Assert.AreEqual(1.0f,
				SceneNodeView.GetLocalScaleY(child));
			Object.DestroyImmediate(parent);
			Object.DestroyImmediate(child);
		}

		[Test]
		public void SetWorldScaleY()
		{
			GameObject parent = new GameObject();
			GameObject child = new GameObject();
			SceneNodeView.AddChild(parent, child);
			SceneNodeView.SetName(parent, "parent");
			SceneNodeView.SetName(child, "child");
			SceneNodeView.SetLocalScaleY(child, 2.0f);
			SceneNodeView.SetLocalScaleY(parent, 3.0f);
			Assert.AreEqual(2.0f,
				SceneNodeView.GetLocalScaleY(child));
			Assert.AreEqual(6.0f,
				SceneNodeView.GetWorldScaleY(child));
			SceneNodeView.SetWorldScaleY(child, 12.0f);
			Assert.AreEqual(1.0f,
				SceneNodeView.GetLocalScaleX(parent));
			Assert.AreEqual(1.0f,
				SceneNodeView.GetLocalScaleX(child));
			Assert.AreEqual(4.0f,
				SceneNodeView.GetLocalScaleY(child));
			Assert.AreEqual(12.0f,
				SceneNodeView.GetWorldScaleY(child));
			Object.DestroyImmediate(parent);
			Object.DestroyImmediate(child);
		}
	}
}
