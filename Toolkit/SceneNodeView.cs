using UnityEngine;
using System.Collections.Generic;

namespace Finegamedesign.Utils
{
	// Portable API
	public sealed class SceneNodeView
	{
		public static GameObject GetChild(GameObject parent, string name)
		{
			Transform transform = parent.transform.Find(name);
			if (null == transform)
			{
				throw new System.InvalidOperationException(
					"Expected child <" + name + "> in " 
					+ parent.transform.parent + parent );
			}
			GameObject child = transform.gameObject;
			return child;
		}

		public static GameObject GetParent(GameObject child)
		{
			return child.transform.parent.gameObject;
		}

		// Preserve local position.
		public static void AddChild(GameObject parent, GameObject child)
		{
			child.transform.SetParent(parent.transform, false);
		}

		public static List<GameObject> GetChildren(GameObject parent)
		{
			List<GameObject> children = new List<GameObject>();
			foreach (Transform child in parent.transform)
			{
				children.Add(child.gameObject);
			}
			return children;
		}

		public static List<SceneNode> ToSceneNodeList(List<GameObject> viewObjects)
		{
			List<SceneNode> nodes = new List<SceneNode>();
			for (int index = 0; index < DataUtil.Length(viewObjects); index++)
			{
				SceneNode node = new SceneNode();
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

		public static void SetVisible(GameObject gameObject, bool isVisible)
		{
			gameObject.SetActive(isVisible);
		}
	}
}
