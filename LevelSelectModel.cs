using System.Collections.Generic;

namespace Finegamedesign.Utils
{
	// Nested level select.
	// Example:  WordBrain.
	public sealed class LevelSelectModel
	{
		public int context = 0;
		public int levelCount = -1;
		public int levelUnlocked = 0;
		public int levelSelected = 0;
		public int requested = 0;
		public List<int> menus = new List<int>();
		public List<int> menuSelected = new List<int>();
		public List<int> levelsPerItem = new List<int>();
		public int menuIndex = 0;

		public void Setup()
		{
			for (int index = 0; index < DataUtil.Length(menus); index++)
			{
				int menu = menus[index];
				levelsPerItem.Add(1);
				for (int previous = 0; previous < index; previous++)
				{
					levelsPerItem[previous] *= menu;
				}
				menuSelected.Add(-1);
			}
		}

		// Item in menu.
		// Return true if available.
		public bool Select(int itemIndex)
		{
			requested = context;
			if (menuIndex < DataUtil.Length(menus) - 1)
			{
				requested += itemIndex * levelsPerItem[menuIndex];
			}
			else
			{
				requested += itemIndex;
			}
			bool isUnlocked = requested <= levelUnlocked;
			if (isUnlocked)
			{
				levelSelected = requested;
				context = requested;
				menuSelected[menuIndex] = itemIndex;
				menuIndex++;
			}
			return isUnlocked;
		}
	}
}
