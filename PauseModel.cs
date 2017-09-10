namespace Finegamedesign.Utils
{
	public sealed class PauseModel
	{
		private const string NoneState = "none";
		private const string BeginState = "begin";
		private const string EndState = "end";

		private string state = NoneState;

		private bool isPaused;

		public void Pause()
		{
			if (isPaused)
			{
				return;
			}
			isPaused = true;
			state = BeginState;
		}

		public void Resume()
		{
			if (!isPaused)
			{
				return;
			}
			isPaused = false;
			state = EndState;
		}

		public bool GetIsPaused()
		{
			return isPaused;
		}

		public string GetState()
		{
			return state;
		}
	}
}
