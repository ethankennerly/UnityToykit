using System.Collections.Generic;

namespace /*<com>*/Finegamedesign.Utils
{
	// Portable view model of a node in a scene graph.
	public class SceneNodeModel
	{
		// Example: Editor/Tests/TestSceneNode.cs
		// http://stackoverflow.com/a/3309397/1417849
		// No LINQ to be portable syntax.
		public static void SortLeftToRight(List<SceneNodeModel> nodes)
		{
			nodes.Sort(SceneNodeCompareLeftToRight.instance);
		}

		public string name = "";
		public float rotation = 0.0f;
		public float x = 0.0f;
		// As in Unity and Cocos2D, but not Flash, higher value is up-screen.
		public float y = 0.0f;
	}
}
