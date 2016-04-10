using UnityEngine;
using UnityEngine.EventSystems;  // Required when using Event data.

public class ButtonView : MonoBehaviour, IPointerDownHandler  // required interface when using the OnPointerDown method.
{
	public static Controller controller;

	/**
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
	public void OnPointerDown(PointerEventData eventData) 
	{
		controller.OnMouseDown(name);
	}

	/**
	 * Expects collider on each 3D object.
	 * Test case:  2016-02-13 Mouse down on cup.  Expect log.  Got no response.
	 * On mobile, raycast is preferred.  Example:
	 * http://wiki.unity3d.com/index.php/OnTouch
	 */
	public void OnMouseDown()
	{
		controller.OnMouseDown(name);
	}
}
