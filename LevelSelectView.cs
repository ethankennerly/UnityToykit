using UnityEngine;
using System.Collections.Generic;

namespace Finegamedesign.Utils
{
	public sealed class LevelSelectView : MonoBehaviour
	{
		public static LevelSelectView GetInstance()
		{
			return Object.FindObjectOfType<LevelSelectView>();
		}

		public List<List<GameObject>> buttons = new List<List<GameObject>>();
		public List<GameObject> menus = new List<GameObject>();
		public List<GameObject> exitButtons = new List<GameObject>();
		public GameObject animatorOwner;

		public void Setup()
		{
			for (int index = 0; index < DataUtil.Length(menus); index++)
			{
				if (DataUtil.Length(buttons) <= index)
				{
					GameObject menu = menus[index];
					buttons.Add(SceneNodeView.GetChildren(menu, true));
				}
			}
			if (null == animatorOwner)
			{
				animatorOwner = gameObject;
			}
		}
	}
}
