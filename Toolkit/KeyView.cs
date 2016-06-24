using UnityEngine;

namespace Finegamedesign.Utils
{
	public sealed class KeyView
	{
		/**
		 * If just pressed this frame.
		 */
		public static bool IsDownNow(string keyName)
		{
			return Input.GetKeyDown(keyName);
		}
	}
}
