using System.Collections.Generic;
using UnityEngine;

namespace Finegamedesign.Utils
{
	public sealed class KeyView
	{
		public static bool isVerbose = false;
		public static string backspaceCharacter = "\b";
		public static string newlineCharacter = "\n";

		// If just pressed this frame.
		// Key naming conventions:
		// http://docs.unity3d.com/Manual/ConventionalGameInput.html
		// List of key names:
		// http://answers.unity3d.com/questions/762073/c-list-of-string-name-for-inputgetkeystring-name.html
		public static bool IsDownNow(string keyName)
		{
			return Input.GetKeyDown(keyName);
		}

		// Represents enter key by "\n"
		// Represents delete key or backspace key by "\b"
		// Unity inserts backspace character already but not delete key.
		public static string InputString()
		{
			string input = Input.inputString;
			if (Input.GetKeyDown("delete"))
			{
				input += backspaceCharacter;
			}
			if (Input.GetKeyDown("return"))
			{
				input += newlineCharacter;
			}
			if (isVerbose && input != null && input != "")
			{
				DebugUtil.Log("KeyView.InputString: <" + input + ">");
			}
			return input;
		}

		// Character type is more efficient, but string type is more compatible with most data.
		public static List<string> InputList()
		{
			return DataUtil.Split(InputString(), "");
		}
	}
}
