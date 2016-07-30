using UnityEngine/*<Screen>*/;

namespace Finegamedesign.Utils
{
	public sealed class ScreenView
	{
		public static bool IsPortrait()
		{
			return Screen.width < Screen.height;
		}
	}
}
