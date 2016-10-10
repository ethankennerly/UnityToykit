using UnityEngine;

namespace Finegamedesign.Utils
{
	public sealed class KeyView
	{
		// If just pressed this frame.
		// Key naming conventions:
		// http://docs.unity3d.com/Manual/ConventionalGameInput.html
		// List of key names:
		// http://answers.unity3d.com/questions/762073/c-list-of-string-name-for-inputgetkeystring-name.html
		public static bool IsDownNow(string keyName)
		{
			return Input.GetKeyDown(keyName);
		}

		public static string InputString()
		{
			return Input.inputString;
		}
	}
}
