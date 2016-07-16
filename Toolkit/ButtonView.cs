using UnityEngine;

namespace Finegamedesign.Utils
{
	public sealed class ButtonView
	{
		public ButtonController controller;

		/**
		 * Add EventSystem to scene graph hierarchy.
		 * http://answers.unity3d.com/questions/954919/onpointerdown-and-ui-buttons.html
		 *
		 * Test case:  Test.  Port.  Change view.  Expect minimal view code.  
		 *
		 * Test case:  2015 Another person is locking the editor file.  I want to edit the code of the button callback.
		 *
		 * Test case:  2013 Players want immediate responsiveness, as soon as the button is pressed.
		 *
		 * Workaround for Unity:
		 * What is a more convenient technique to call OnMouseDown?
		 * What is a more convenient technique to add listener outside of the editor?
		 * Adapted from:
		 * http://answers.unity3d.com/questions/829594/ui-button-onmousedown.html
		 * http://docs.unity3d.com/ScriptReference/UI.Selectable.OnPointerDown.html
		 */
		public void Listen(GameObject button)
		{
			button.AddComponent<ButtonBehaviour>();
			ButtonBehaviour behaviour = button.GetComponent<ButtonBehaviour>();
			behaviour.controller = controller;
		}
	}
}
