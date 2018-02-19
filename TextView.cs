using System.Collections.Generic;
using TMPro;
using UnityEngine/*<GameObject>*/;
using UnityEngine.UI/*<Text>*/;

namespace Finegamedesign.Utils
{
    public static class TextView
    {
        public static string GetText(TextMeshPro textMesh)
        {
            return textMesh.text;
        }

        public static string GetText(TextMeshProUGUI textMesh)
        {
            return textMesh.text;
        }

        public static void SetText(TextMeshPro textMesh, string text)
        {
            textMesh.text = text;
        }

        public static void SetTexts(List<TextMeshPro> textOwners, List<string> texts)
        {
            for (int index = 0, end = texts.Count; index < end; ++index)
            {
                SetText(textOwners[index], texts[index]);
            }
        }

        // Try Text first, then TextMesh, then child named "Text".  Else error.
        // Return true if text component was found.
        public static string GetText(GameObject textOwner, string childName = "Text")
        {
            TextMeshPro tmp = textOwner.GetComponent<TextMeshPro>();
            if (tmp != null)
            {
                return tmp.text;
            }
            TextMeshProUGUI tmpUI = textOwner.GetComponent<TextMeshProUGUI>();
            if (tmpUI != null)
            {
                return tmpUI.text;
            }
            Text textComponent = textOwner.GetComponent<Text>();
            if (textComponent != null)
            {
                return textComponent.text;
            }
            TextMesh mesh = textOwner.GetComponent<TextMesh>();
            if (mesh != null)
            {
                return mesh.text;
            }
            GameObject child = SceneNodeView.GetChild(textOwner, childName);
            if (child != null)
            {
                return GetText(child, childName);
            }
            throw new System.InvalidOperationException(
                "Expected to set Text or TextMesh component on " + textOwner);
        }

        // Try Text first, then TextMesh, then child named "Text".  Else error.
        // Return true if text component was found.
        public static bool SetText(GameObject textOwner, string text, string childName = "Text")
        {
            Text textComponent = textOwner.GetComponent<Text>();
            if (textComponent != null)
            {
                textComponent.text = text;
                return true;
            }
            TextMesh mesh = textOwner.GetComponent<TextMesh>();
            if (mesh != null)
            {
                mesh.text = text;
                return true;
            }
            GameObject child = SceneNodeView.GetChild(textOwner, childName);
            if (child != null)
            {
                return SetText(child, text, childName);
            }
            throw new System.InvalidOperationException(
                "Expected to set Text or TextMesh component on " + textOwner);
        }

        public static void SetTexts(List<GameObject> textOwners, List<string> texts, string childName = "Text")
        {
            for (int index = 0, end = texts.Count; index < end; ++index)
            {
                SetText(textOwners[index], texts[index], childName);
            }
        }

        public static void SetChildText(GameObject textOwnerParent, string text, string childName = "Text")
        {
            SetText(textOwnerParent, text, childName);
        }
    }
}
