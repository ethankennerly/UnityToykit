namespace Finegamedesign.Utils
{
	public sealed class LevelSelectController
	{
		public LevelSelectModel model = new LevelSelectModel();
		public ButtonController buttons = new ButtonController();

		public void Setup()
		{
			model.Setup();
		}

		public void Update()
		{
			buttons.Update();
			if (buttons.isAnyNow)
			{
				int index = StringUtil.ParseIndex(buttons.downName);
				if (0 <= index)
				{
					model.Select(index);
				}
			}
		}
	}
}
