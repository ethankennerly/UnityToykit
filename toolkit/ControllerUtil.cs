using UnityEngine;  // GameObject data type
using UnityEngine.UI;  // Text data type
using System.Collections.Generic;  // Dictionary

public class ControllerUtil
{
	public static string[] keys = new string[]{
		"state", "text"};

	/**
	 * Find the children game objects in the scene graph at the addresses from the view model's scene graph.
	 * @param	rootAddress	The root will not be in the graph.  The root's children will be the values of the top-level hash.  
	 */
	public static Dictionary<string, GameObjectTree> FindGraph(Dictionary<string, object> graph, GameObject root)
	{
		Dictionary<string, GameObjectTree> sceneGraph = new Dictionary<string, GameObjectTree>();
		foreach (KeyValuePair<string, object> item in graph) {
			string name = item.Key;
			GameObject child = ViewUtil.GetChild(root, name);
			GameObjectTree node = new GameObjectTree(child);
			sceneGraph[name] = node;
			if (null == child) {
				Debug.Log("Expected child at " + name);
			}
			else if (item.Value is Dictionary<string, object>) {
				Dictionary<string, object> subGraph = (Dictionary<string, object>) item.Value;
				sceneGraph[item.Key].children = FindGraph(subGraph, child);
			}
		}
		return sceneGraph;
	}

	/**
	 * @param	rootAddress	Expects root address is unique among instantiated prefabs.
	 */
	public static Dictionary<string, GameObjectTree> FindGraphByName(Dictionary<string, object> graph, string rootAddress)
	{
		GameObject root = GameObject.Find(rootAddress);
		return FindGraph(graph, root);
	}

	/**
	 * @param	news	Conveniently construct nested hashes inside of the already constructed news.  If an ancestor is already in news then merge.
	 */
	public static void SetNews(Dictionary<string, object> news, string[] address, string state, string key = "state")
	{
		Dictionary<string, object> parent = news;
		for (int index = 0; index < address.Length; index++) {
			string name = address[index];
			if (!parent.ContainsKey(name)) {
				parent[name] = new Dictionary<string, object>();
			}
			parent = (Dictionary<string, object>) parent[name];
		}
		parent[key] = state;
	}

	/**
	 * @param	news	If key "state" in news, set descending child to that state.  Recurses news and descendents.  Side effect:  Clears all news.  
	 *
	 * Flash scene graph (called the display list) has movie clips and other display objects, which are automatically assigned member variables of their children when they have a name.  So a JSON shorthand can be employed like {"cup_0": "empty"}.  Unity GameObject is a sealed class that cannot be extended, so instead the corresponding syntax is {"cup_0": {"state": "empty"}}.  C# is strictly typed, and dictionaries are recommended over hashtables, so the syntax is actually much more verbose:  new Dictionary<string, object>(){{"cup_0", new Dictionary<string, object>(){{"state", "empty"}}}}
	 */
	public static void SetStates(Dictionary<string, object> news, Dictionary<string, GameObjectTree> descendents)
	{
		GameObjectTree tree;
		GameObject child;
		foreach (KeyValuePair<string, object> item in news) {
			tree = descendents[item.Key];
			child = tree.self;
			Dictionary<string, object> articles = (Dictionary<string, object>) item.Value;
			for (int index = 0; index < keys.Length; index++) {
				string key = keys[index];
				if (articles.ContainsKey(key)) {
					string state = (string) articles[key];
					if ("state" == key) {
						ViewUtil.SetState(child, state, true); 
					}
					else if ("text" == key) {
						Text uiText = child.GetComponent<Text>();
						ViewUtil.SetText(uiText, state); 
					}
					articles.Remove(key);
				}
			}
			if (1 <= articles.Count) {
				SetStates(articles, tree.children);
			}
		}
		news.Clear();
	}

	public static void SetStateArray(GameObject[] gameObjects, string[] states, bool isRestart = false)
	{
		for (int index = 0; index < gameObjects.Length; index++)
		{
			GameObject gameObject = gameObjects[index];
			string state = states[index];
			ViewUtil.SetState(gameObject, state, isRestart);
		}
	}

	public static void SetupButtons(Controller controller, string[] addresses)
	{
		for (int index = 0; index < addresses.Length; index++) {
			string address = addresses[index];
			ViewUtil.SetupButton(controller, address);
		}
	}

	public static void PlaySounds(List<string> soundBaseNames)
	{
		for (int index = 0; index < soundBaseNames.Count; index++) {
			string filename = soundBaseNames[index];
			ViewUtil.PlaySound(filename);
		}
		soundBaseNames.Clear();
	}
}
