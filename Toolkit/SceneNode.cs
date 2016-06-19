using System.Collections.Generic;

namespace com.finegamedesign.utils
{
	// Portable view model of a node in a scene graph.
	public class SceneNode
	{
		// Example: Editor/Tests/TestSceneNode.cs
		// http://stackoverflow.com/a/3309397/1417849
		// No LINQ to be portable syntax.
		public static void SortLeftToRight(List<SceneNode> nodes)
		{
			nodes.Sort(SceneNodeCompareLeftToRight.instance);
		}

		public string name = "";
		public float x = 0.0f;
		// As in Unity and Cocos2D, but not Flash, higher value is up-screen.
		public float y = 0.0f;
	}
}
