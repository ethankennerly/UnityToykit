using UnityEngine/*<GameObject>*/;
using UnityEngine.UI/*<Text>*/;

namespace Finegamedesign.Utils
{
	public sealed class TextView
	{
		public static string GetText(GameObject textOwner)
		{
			Text textComponent = textOwner.GetComponent<Text>();
			return textComponent.text;
		}

		public static void SetText(GameObject textOwner, string text)
		{
			Text textComponent = textOwner.GetComponent<Text>();
			textComponent.text = text;
		}
	}
}
