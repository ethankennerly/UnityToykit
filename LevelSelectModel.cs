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
		public List<string> menuNames = new List<string>();
		public List<int> levelsPerItem = new List<int>();
		public int menuIndex = 0;
		public string menuName;

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
			SetMenuName(menuIndex);
		}

		private void SetMenuName(int menuIndex)
		{
			if (menuIndex < DataUtil.Length(menuNames))
			{
				menuName = menuNames[menuIndex];
			}
		}

		private int Amount(int itemIndex)
		{
			int amount;
			if (menuIndex < DataUtil.Length(menus) - 1)
			{
				amount = itemIndex * levelsPerItem[menuIndex];
			}
			else
			{
				amount = itemIndex;
			}
			return amount;
		}

		// Item in menu.
		// Return true if available.
		public bool Select(int itemIndex)
		{
			requested = context + Amount(itemIndex);
			bool isUnlocked = requested <= levelUnlocked;
			if (isUnlocked)
			{
				levelSelected = requested;
				context = requested;
				menuSelected[menuIndex] = itemIndex;
				menuIndex++;
				SetMenuName(menuIndex);
			}
			return isUnlocked;
		}

		public void Exit()
		{
			if (1 <= menuIndex)
			{
				menuIndex--;
				context -= Amount(menuSelected[menuIndex]);
				SetMenuName(menuIndex);
			}
		}
	}
}
