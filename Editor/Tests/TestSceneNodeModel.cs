using System.Collections.Generic;
using NUnit.Framework;

namespace /*<com>*/Finegamedesign.Utils
{
	[TestFixture]
	public class TestSceneNodeModel
	{
		[Test]
		public void SortLeftToRight()
		{
			SceneNodeModel a = new SceneNodeModel();
			a.x = 1.0f;
			a.y = 1.0f;
			a.name = "node_0";
			SceneNodeModel b = new SceneNodeModel();
			b.x = 2.0f;
			b.y = 1.0f;
			b.name = "node_1";
			SceneNodeModel c = new SceneNodeModel();
			c.x = 2.0f;
			c.y = 0.0f;
			c.name = "node_2";
			List<SceneNodeModel> nodes = new List<SceneNodeModel>(){c, a, b};
			SceneNodeModel.SortLeftToRight(nodes);
			Assert.AreEqual(a.name, nodes[0].name);
			Assert.AreEqual(a, nodes[0]);
			Assert.AreEqual(b.name, nodes[1].name);
			Assert.AreEqual(b, nodes[1]);
			Assert.AreEqual(c.name, nodes[2].name);
			Assert.AreEqual(c, nodes[2]);
		}
	}
}
