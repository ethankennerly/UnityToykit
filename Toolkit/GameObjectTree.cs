using UnityEngine;  // GameObject
using System.Collections.Generic;  // Dictionary

public class GameObjectTree
{
	public GameObject self;
	public Dictionary<string, GameObjectTree> children = new Dictionary<string, GameObjectTree>();

	public GameObjectTree(GameObject child)
	{
		self = child;
	}
}
