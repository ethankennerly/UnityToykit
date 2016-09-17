namespace Finegamedesign.Utils
{
	public sealed class LevelSelectController
	{
		public LevelSelectModel model = new LevelSelectModel();
		public LevelSelectView view;
		public ButtonController buttons = new ButtonController();

		// Listen to menu buttons and exit buttons.
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
			int exitButtonLength = DataUtil.Length(view.exitButtons);
			for (int index = 0; index < exitButtonLength; index++)
			{
				var exitButton = view.exitButtons[index];
				if (null != exitButton)
				{
					buttons.view.Listen(exitButton);
				}
			}
		}

		public void ViewButtons()
		{
			if (model.IsInMenu())
			{
				var items = view.buttons[model.menuIndex];
				for (int index = 0; index < DataUtil.Length(items); index++)
				{
					bool isVisible = model.IsInRange(index);
					var item = items[index];
					SceneNodeView.SetVisible(item, isVisible);
					ButtonView.SetEnabled(item, model.IsUnlocked(index));
					TextView.SetChildText(item, model.LevelName(index));
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
					else
					{
						index = view.exitButtons.IndexOf(buttons.view.target);
						if (0 <= index)
						{
							model.Exit();
						}
					}
				}
			}
			ViewButtons();
			if (null != view && null != model.menuName)
			{
				AnimationView.SetState(view.animatorOwner, model.menuName);
			}
		}
	}
}
