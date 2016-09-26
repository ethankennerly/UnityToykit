using System.Collections.Generic;

namespace Finegamedesign.Utils
{
	// Nested level select.
	// Example:  WordBrain.
	public sealed class LevelSelectModel
	{
		public int context = 0;
		public int levelCount = -1;
		public int levelCurrently = 0;
		public int levelUnlocked = 0;
		public int levelSelected = 0;
		public int requested = 0;
		public List<int> menus = new List<int>();
		public List<int> menuSelected = new List<int>();
		public List<string> menuNames = new List<string>();
		public List<int> levelsPerItem = new List<int>();
		public int menuIndex = 0;
		public string menuName;
		public Watcher<bool> inMenu;

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
			inMenu = Watcher<bool>.Create(IsInMenu());
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

		public int LevelIndex(int itemIndex)
		{
			return context + Amount(itemIndex);
		}

		public string LevelName(int itemIndex)
		{
			int levelNumber = LevelIndex(itemIndex) + 1;
			return levelNumber.ToString();
		}

		public bool IsUnlocked(int itemIndex)
		{
			requested = LevelIndex(itemIndex);
			return requested <= levelUnlocked;
		}

		public bool IsInRange(int itemIndex)
		{
			int level = LevelIndex(itemIndex);
			return 0 <= level && level < levelCount;
		}

		// Item in menu.
		// Return true if available.
		public bool Select(int itemIndex)
		{
			if (IsUnlocked(itemIndex))
			{
				levelSelected = requested;
				levelCurrently = requested;
				context = requested;
				menuSelected[menuIndex] = itemIndex;
				menuIndex++;
				SetMenuName(menuIndex);
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool IsInMenu()
		{
			return 0 <= menuIndex && menuIndex < DataUtil.Length(menus);
		}

		// Exit to level select screen.  
		// Return to current level, which may not be the level selected.
		// Test case:  2016-09-18 Press X.  Select level.  
		// Jennifer Russ could expect highest level.  Got first level selected.
		public void Exit()
		{
			if (1 <= menuIndex)
			{
				menuIndex--;
				context = ContextCurrently();
				// context -= Amount(menuSelected[menuIndex]);
				SetMenuName(menuIndex);
			}
		}

		private int ContextCurrently()
		{
			int currently = 0;
			if (1 <= menuIndex)
			{
				int rounding = levelsPerItem[menuIndex - 1];
				currently = levelCurrently / rounding * rounding;
			}
			return currently;
		}

		public void Update()
		{
			inMenu.Update(IsInMenu());
		}
	}
}
