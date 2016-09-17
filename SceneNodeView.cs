using UnityEngine;
using System.Collections.Generic;

namespace Finegamedesign.Utils
{
	// Portable API
	public sealed class SceneNodeView
	{
		public static GameObject Find(string path)
		{
			return GameObject.Find(path);
		}

		public static object FindObjectOfType(System.Type type)
		{
			return Object.FindObjectOfType(type);
		}

		// Override child:  If not null, return this instead.
		// Useful for editor to override game objects if needed.
		public static GameObject GetChild(GameObject parent, string name, GameObject overrideChild = null)
		{
			if (null != overrideChild)
			{
				return overrideChild;
			}
			Transform transform = parent.transform.Find(name);
			if (null == transform)
			{
				throw new System.InvalidOperationException(
					"Expected child <" + name + "> in <" 
					+ GetPath(parent) + ">");
			}
			GameObject child = transform.gameObject;
			return child;
		}

		// namePattern:  Substitute {0} with an index between 0 and to countMax - 1.
		public static List<GameObject> GetChildrenByPattern(GameObject parent, string namePattern, int countMax) 
		{
			List<GameObject> children = new List<GameObject>();
			for (int i = 0; i < countMax; i++)
			{
				string name = namePattern.Replace("{0}", i.ToString());
				GameObject child = SceneNodeView.GetChild(parent, name);
				children.Add(child);
			}
			return children;
		}

		public static GameObject GetParent(GameObject child)
		{
			return child.transform.parent.gameObject;
		}

		public static string GetPath(GameObject child)
		{
			string path = child.name;
			GameObject parent = child;
			while (null != parent.transform.parent)
			{
				parent = parent.transform.parent.gameObject;
				path = parent.name + "/" + path;
			}
			return path;
		}

		// Preserve local position.
		public static void AddChild(GameObject parent, GameObject child)
		{
			child.transform.SetParent(parent.transform, false);
		}

		public static List<GameObject> GetChildren(GameObject parent, bool isIncludeActiveOnly = false)
		{
			List<GameObject> children = new List<GameObject>();
			foreach (Transform child in parent.transform)
			{
				if (!isIncludeActiveOnly || child.gameObject.activeSelf)
				{
					children.Add(child.gameObject);
				}
			}
			return children;
		}

		public static List<SceneNodeModel> ToSceneNodeList(List<GameObject> viewObjects)
		{
			List<SceneNodeModel> nodes = new List<SceneNodeModel>();
			for (int index = 0; index < DataUtil.Length(viewObjects); index++)
			{
				SceneNodeModel node = new SceneNodeModel();
				GameObject viewObject = viewObjects[index];
				node.name = GetName(viewObject);
				node.x = GetLocalX(viewObject);
				node.y = GetLocalY(viewObject);
				nodes.Add(node);
			}
			return nodes;
		}

		public static string GetName(GameObject viewObject)
		{
			return viewObject.name;
		}

		public static void SetName(GameObject viewObject, string name)
		{
			viewObject.name = name;
		}

		public static float GetLocalX(GameObject viewObject)
		{
			return viewObject.transform.localPosition.x;
		}

		public static float GetLocalY(GameObject viewObject)
		{
			return viewObject.transform.localPosition.y;
		}

		// Unity does not update unless new Vector3 created.
		// Test case:  Assign directly.  Nothing happens.
		public static void SetLocalX(GameObject viewObject, float x)
		{
			Vector3 current = viewObject.transform.localPosition;
			Vector3 localPosition = new Vector3(
				x, current.y, current.z);
			viewObject.transform.localPosition = localPosition;
		}

		public static void SetLocalY(GameObject viewObject, float y)
		{
			Vector3 current = viewObject.transform.localPosition;
			Vector3 localPosition = new Vector3(
				current.x, y, current.z);
			viewObject.transform.localPosition = localPosition;
		}

		// Unity will not change if localPosition refers to this viewObject.
		// If that would be the case, make a new local position.
		public static void SetLocal(GameObject viewObject, Vector3 localPosition)
		{
			if (localPosition == viewObject.transform.localPosition)
			{
				localPosition = new Vector3(
					localPosition.x, 
					localPosition.y,
					localPosition.z);
			}
			viewObject.transform.localPosition = localPosition;
		}

		public static float GetWorldX(GameObject viewObject)
		{
			return viewObject.transform.position.x;
		}

		public static float GetWorldY(GameObject viewObject)
		{
			return viewObject.transform.position.y;
		}

		// Unity does not update unless new Vector3 created.
		// Test case:  Assign directly.  Nothing happens.
		public static void SetWorldX(GameObject viewObject, float x)
		{
			Vector3 current = viewObject.transform.position;
			Vector3 position = new Vector3(
				x, current.y, current.z);
			viewObject.transform.position = position;
		}

		public static void SetWorldY(GameObject viewObject, float y)
		{
			Vector3 current = viewObject.transform.position;
			Vector3 position = new Vector3(
				current.x, y, current.z);
			viewObject.transform.position = position;
		}

		public static float GetLocalScaleX(GameObject viewObject)
		{
			return viewObject.transform.localScale.x;
		}

		public static float GetLocalScaleY(GameObject viewObject)
		{
			return viewObject.transform.localScale.y;
		}

		// Unity does not update unless new Vector3 created.
		// Test case:  Assign directly.  Nothing happens.
		public static void SetLocalScaleX(GameObject viewObject, float x)
		{
			Vector3 current = viewObject.transform.localScale;
			Vector3 localScale = new Vector3(
				x, current.y, current.z);
			viewObject.transform.localScale = localScale;
		}

		public static void SetLocalScaleY(GameObject viewObject, float y)
		{
			Vector3 current = viewObject.transform.localScale;
			Vector3 localScale = new Vector3(
				current.x, y, current.z);
			viewObject.transform.localScale = localScale;
		}

		public static float GetWorldScaleX(GameObject viewObject)
		{
			return viewObject.transform.lossyScale.x;
		}

		public static float GetWorldScaleY(GameObject viewObject)
		{
			return viewObject.transform.lossyScale.y;
		}

		// Unity does not update unless new Vector3 created.
		// Test case:  Assign directly.  Nothing happens.
		public static void SetWorldScaleX(GameObject viewObject, float x)
		{
			float world = GetWorldScaleX(viewObject);
			float local = GetLocalScaleX(viewObject);
			SetLocalScaleX(viewObject, x * local / world);
		}

		public static void SetWorldScaleY(GameObject viewObject, float y)
		{
			float world = GetWorldScaleY(viewObject);
			float local = GetLocalScaleY(viewObject);
			SetLocalScaleY(viewObject, y * local / world);
		}

		public static bool GetVisible(GameObject gameObject)
		{
			return gameObject.activeSelf;
		}

		public static void SetVisible(GameObject gameObject, bool isVisible)
		{
			gameObject.SetActive(isVisible);
		}
	}
}
