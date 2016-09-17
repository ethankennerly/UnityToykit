using UnityEngine;
using UnityEngine.UI/*<Button>*/;

namespace Finegamedesign.Utils
{
	public sealed class ButtonView
	{
		public static void SetEnabled(GameObject buttonOwner, bool isEnabled)
		{
			Button button = buttonOwner.GetComponent<Button>();
			button.interactable = isEnabled;
		}

		public ButtonController controller;
		public GameObject target = null;
		public GameObject targetNext = null;

		//
		// Add EventSystem to scene graph hierarchy.
		// http://answers.unity3d.com/questions/954919/onpointerdown-and-ui-buttons.html
		// 
		// Test case:  Test.  Port.  Change view.  Expect minimal view code.  
		// 
		// Test case:  2015 Another person is locking the editor file.  I want to edit the code of the button callback.
		// 
		// Test case:  2013 Players want immediate responsiveness, as soon as the button is pressed.
		// 
		// Workaround for Unity:
		// What is a more convenient technique to call OnMouseDown?
		// What is a more convenient technique to add listener outside of the editor?
		// Adapted from:
		// http://answers.unity3d.com/questions/829594/ui-button-onmousedown.html
		// http://docs.unity3d.com/ScriptReference/UI.Selectable.OnPointerDown.html
		// 
		public void Listen(GameObject button)
		{
			ButtonBehaviour behaviour = button.GetComponent<ButtonBehaviour>();
			if (null == behaviour)
			{
				button.AddComponent<ButtonBehaviour>();
				behaviour = button.GetComponent<ButtonBehaviour>();
			}
			behaviour.view = this;
		}

		public void Down(GameObject button)
		{
			targetNext = button;
			controller.Down(button.name);
		}
	}
}
