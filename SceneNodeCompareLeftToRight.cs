using System.Collections.Generic;

namespace /*<com>*/Finegamedesign.Utils
{
	// Portable view model of a node in a scene graph.
	public class SceneNodeCompareLeftToRight : IComparer<SceneNodeModel>
	{
		public static SceneNodeCompareLeftToRight instance = new SceneNodeCompareLeftToRight();

		// If tied, up-screen first.
		public int Compare(SceneNodeModel a, SceneNodeModel b)
		{
			int left = a.x.CompareTo(b.x);
			if (0 == left)
			{
				int up = b.y.CompareTo(a.y);
				return up;
			}
			return left;
		}
	}
}
