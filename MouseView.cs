using UnityEngine;

namespace Finegamedesign.Utils
{
	public sealed class MouseView
	{
		public static GameObject target;

		//
		// Mouse (or touch) selects first game object in the world that has a collider.
		// 
		public static void Update()
		{
			target = null;
			if (Input.GetMouseButtonDown(0)) {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit)) {
					target = hit.transform.gameObject;
				}
			}
		}
	}
}
