using UnityEngine/*<GameObject>*/;
using System.Collections.Generic/*<Dictionary>*/;

namespace Finegamedesign.Utils
{
	public sealed class AnimationView
	{
		public static bool isVerbose = true;
		private static Dictionary<GameObject, string> states = new Dictionary<GameObject, string>();
		private static Dictionary<string, float> startTimes = new Dictionary<string, float>();

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
		public static void SetState(GameObject animatorOwner, string state, bool isRestart = false)
		{
			Animator animator = animatorOwner.GetComponent<Animator>();
			if (null != animator && animator.isInitialized)
			{
				if (isVerbose)
				{
					Debug.Log("AnimationView.SetState: " 
						+ SceneNodeView.GetPath(animatorOwner)
						+ ": " + state + " at " + Time.time);
				}
				if (isRestart)
				{
					animator.Play(state, -1, 0f);
				}
				else
				{
					animator.Play(state);
				}
				states[animatorOwner] = state;
				startTimes[state] = Time.time;
			}
			else
			{
				if (null == animator) {
					Debug.Log("AnimationView.SetState: Does animator exist? " 
						+ SceneNodeView.GetPath(animatorOwner)
						+ ": " + state);
				}
			}
		}

		/**
		 * Return name state that was completed now, or null.
		 * Also erases that state, so next time this is called it won't be completed now.
		 * Expects time passed since SetState to avoid race when CompletedNow is not called before SetState in the frame.
		 * Expects animation is on layer 0.
		 * Syncrhonous. Does not depend on event complete animation.
		 *
		 * http://answers.unity3d.com/questions/351534/how-to-get-current-state-on-mecanim.html
		 *
		 * Blubberfish says integers will compare faster than strings.
		 * http://answers.unity3d.com/questions/407186/how-to-get-current-state-name-on-mecanim.html
		 */
		public static string CompletedNow(GameObject animatorOwner, int layer = 0)
		{
			string completedNow = null;
			Animator animator = animatorOwner.GetComponent<Animator>();
			if (null != animator && animator.isInitialized)
			{
				AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(layer);
				if (states.ContainsKey(animatorOwner) && null != states[animatorOwner] 
				&& Time.time != startTimes[states[animatorOwner]]
				&& 1.0f < info.normalizedTime
				)
				{
					completedNow = states[animatorOwner];
					if (isVerbose)
					{
						Debug.Log("AnimationView.CompletedNow: " 
							+ SceneNodeView.GetPath(animatorOwner)
							+ ": " + completedNow + " at " + Time.time 
							+ " info " + info 
							+ " IsName " + info.IsName(states[animatorOwner])
							+ " length " + info.length
							+ " normalizedTime " + info.normalizedTime
						);
						Animation animationStates = animator.GetComponent<Animation>();
						if (null != animationStates)
						{
							foreach(AnimationState state in animationStates)
							{
								Debug.Log("    state " + state.name 
									+ " time " + state.time 
									+ " length " + state.length
									+ " weight " + state.weight
									+ " enabled " + state.enabled
									+ " normalizedTime " + state.normalizedTime
								);
							}
						}
						AnimatorClipInfo[] clips = animator.GetCurrentAnimatorClipInfo(0);
						foreach(AnimatorClipInfo clip in clips)
						{
							Debug.Log("clip " + clip.clip.name + " length " + clip.clip.length);
						}
					}
					states[animatorOwner] = null;
				}
			}
			else
			{
				if (null == animator) {
					Debug.Log("AnimationView.CompletedNow: Does animator exist? " 
						+ SceneNodeView.GetPath(animatorOwner));
				}
			}
			return completedNow;
		}
	}
}
