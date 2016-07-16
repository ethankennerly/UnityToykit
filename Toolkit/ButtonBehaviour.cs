using UnityEngine;
using UnityEngine.EventSystems/*<PointerEventData>*/;

namespace Finegamedesign.Utils
{
	// Assigned by ButtonView.Listen
	// Interface required when using the OnPointerDown method.
	public sealed class ButtonBehaviour : MonoBehaviour, IPointerDownHandler
	{
		public ButtonController controller;

		public void OnPointerDown(PointerEventData eventData) 
		{
			controller.Down(name);
		}
	}
}
