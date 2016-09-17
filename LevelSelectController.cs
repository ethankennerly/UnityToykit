namespace Finegamedesign.Utils
{
	public sealed class LevelSelectController
	{
		public LevelSelectModel model = new LevelSelectModel();
		public LevelSelectView view;
		public ButtonController buttons = new ButtonController();

		public void Setup()
		{
			model.Setup();
			view = LevelSelectView.GetInstance();
			if (null != view)
			{
				view.Setup();
			}
			for (int menu = 0; menu < DataUtil.Length(model.menus); menu++)
			{
				var items = view.buttons[menu];
				for (int item = 0; item < DataUtil.Length(items); item++)
				{
					buttons.view.Listen(items[item]);
				}
			}
		}

		public void Update()
		{
			buttons.Update();
			model.Update();
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
			if (null != view && null != model.menuName)
			{
				AnimationView.SetState(view.animatorOwner, model.menuName);
			}
		}
	}
}
