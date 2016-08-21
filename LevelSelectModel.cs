using System.Collections.Generic;

namespace Finegamedesign.Utils
{
	public sealed class LevelSelectModel
	{
		public int levelCount = -1;
		public int levelUnlocked = 0;
		public int menus = new List<int>();

		// Item in menu.
		// Return true if available.
		public bool Select(int index)
		{
		}
	}
}
