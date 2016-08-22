namespace Finegamedesign.Utils
{
	public sealed class LevelSelectController
	{
		public LevelSelectModel model = new LevelSelectModel();
		public LevelSelectView view = new LevelSelectView();
		public ButtonController buttons = new ButtonController();

		public void Setup()
		{
			model.Setup();
			view = LevelSelectView.GetInstance();
			if (null != view)
			{
				view.Setup();
			}
		}

		public void Update()
		{
			buttons.Update();
			if (buttons.isAnyNow)
			{
				int index = -1;
				if (model.IsInMenu())
				{
					if (null != view)
					{
						var items = view.buttons[model.menuIndex];
						index = items.IndexOf(buttons.view.target);
					}
					if (index <= -1)
					{
						index = StringUtil.ParseIndex(buttons.downName);
					}
					if (0 <= index)
					{
						model.Select(index);
					}
				}
			}
		}
	}
}
