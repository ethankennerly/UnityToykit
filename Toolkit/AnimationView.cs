using UnityEngine/*<GameObject>*/;
using System.Collections.Generic/*<Dictionary>*/;

namespace Finegamedesign.Utils
{
	public sealed class AnimationView
	{
		/**
		 * Call animator.Play instead of animator.SetTrigger, in case the animator is in transition.
		 * Test case:  2015-11-15 Enter "SAT".  Type "RAT".  Expect R selected.  Got "R" resets to unselected.
		 * http://answers.unity3d.com/questions/801875/mecanim-trigger-getting-stuck-in-true-state.html
		 *
		 * Do not call until initialized.  Test case:  2015-11-15 Got warning "Animator has not been initialized"
		 * http://answers.unity3d.com/questions/878896/animator-has-not-been-initialized-1.html
		 *
		 * In editor, deleted and recreated animator state transition.  Test case:  2015-11-15 Got error "Transition '' in state 'selcted' uses parameter 'none' which is not compatible with condition type"
		 * http://answers.unity3d.com/questions/1070010/transition-x-in-state-y-uses-parameter-z-which-is.html
		 *
		 * Unity expects not to animate the camera or the root itself.  Instead animate the child of the root.  The root might not move.
		 * Test case:  2016-02-13 Animate camera position.  Play.  Camera does not move.  Generate root motion curves.  Apply root motion curves.  Still camera does not move.  Assign animator to parent of camera.  Animate child.  Then camera moves.
		 */
		public static void SetState(GameObject gameObject, string state, bool isRestart = true)
		{
			Animator animator = gameObject.GetComponent<Animator>();
			if (null != animator && animator.isInitialized)
			{
				// Debug.Log("AnimationView.SetState: " + gameObject + ": " + state);
				if (isRestart)
				{
					animator.Play(state);
				}
				else
				{
					animator.Play(state, -1, 0f);
				}
			}
			else
			{
				if (null == animator) {
					Debug.Log("AnimationView.SetState: Does animator exist? " + gameObject.transform.parent + gameObject + ": " + state);
				}
			}
		}
	}
}
