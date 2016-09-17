using UnityEngine/*<GameObject>*/;
using UnityEngine.UI/*<Text>*/;

namespace Finegamedesign.Utils
{
	public sealed class TextView
	{
		// Try Text first, then TextMesh.  Else error.
		public static string GetText(GameObject textOwner)
		{
			string text;
			Text textComponent = textOwner.GetComponent<Text>();
			if (null != textComponent)
			{
				text = textComponent.text;
			}
			else
			{
				TextMesh mesh = textOwner.GetComponent<TextMesh>();
				if (null != mesh)
				{
					text = mesh.text;
				}
				else
				{
					throw new System.InvalidOperationException("Expected Text or TextMesh component on " + textOwner);
				}
			}
			return text;
		}

		// Try Text first, then TextMesh.  Else error.
		public static void SetText(GameObject textOwner, string text)
		{
			Text textComponent = textOwner.GetComponent<Text>();
			if (null != textComponent)
			{
				textComponent.text = text;
			}
			else
			{
				TextMesh mesh = textOwner.GetComponent<TextMesh>();
				if (null != mesh)
				{
					mesh.text = text;
				}
				else
				{
					throw new System.InvalidOperationException("Expected to set Text or TextMesh component on " + textOwner);
				}
			}
		}

		public static void SetChildText(GameObject textOwnerParent, string text, string childName = "Text")
		{
			GameObject textOwner = SceneNodeView.GetChild(textOwnerParent, childName);
			SetText(textOwner, text);
		}
	}
}
