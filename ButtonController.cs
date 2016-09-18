namespace Finegamedesign.Utils
{
	// An "Up" would make this portable to ActionScript, which has no mouseDownNow event.
	public sealed class ButtonController
	{
		public bool isAnyNow = false;
		public bool isVerbose = false;
		public string downName = null;
		public ButtonView view = new ButtonView();
		private string downNameNext = null;

		public ButtonController()
		{
			view.controller = this;
		}

		// Down this frame.
		public void Down(string buttonName)
		{
			downNameNext = buttonName;
		}

		// Is any button pressed dowm since last update?
		// What is the name of that button?
		// Example: Editor/Tests/TestButtonController.cs
		public void Update()
		{
			downName = downNameNext;
			isAnyNow = null != downName;
			downNameNext = null;
			view.target = view.targetNext;
			view.targetNext = null;
			if (isVerbose && isAnyNow)
			{
				DebugUtil.Log("ButtonController.Update: " + downName);
			}
		}
	}
}
