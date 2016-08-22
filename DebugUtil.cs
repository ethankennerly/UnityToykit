using UnityEngine/*<Debug>*/;
using System/*<String, StringSplitOptions>*/;

/**
 * Bridge between portable game and platform-specific filesystem, game engine or toolkit.
 * Animation, sound, UI, or other view are in ViewUtils instead.
 */
public sealed class DebugUtil
{
	public static bool isLogEnabled = true;

	public static void Log(string message)
	{
		if (isLogEnabled)
		{
			int ms = (int)(Time.time * 1000.0f);
			Debug.Log(ms.ToString() + "ms " + message);
		}
	}
}
