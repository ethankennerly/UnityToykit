using UnityEngine;

public class MouseView : MonoBehaviour
{
	public static Controller controller;

	/**
	 * Test case:  Test.  Port.  Change view.  Expect minimal view code.  
	 *
	 * Test case:  2015 Another person is locking the editor file.  I want to edit the code of the button callback.
	 *
	 * Test case:  2013 Players want immediate responsiveness, as soon as the button is pressed.
	 *
	 * http://answers.unity3d.com/questions/888132/add-script-to-prefab-at-runtime-and-onmousedown.html
	 */
	public void OnMouseDown()
	{
		controller.OnMouseDown(name);
	}
}
