using UnityEngine/*<GameObject>*/;
using System.Collections.Generic/*<Dictionary>*/;

namespace Finegamedesign.Utils
{
    public static class AnimationView
    {
        public static bool isVerbose = false;
        private static Dictionary<Animator, string> states = new Dictionary<Animator, string>();
        private static Dictionary<string, float> startTimes = new Dictionary<string, float>();

        private static List<Animator> animatorsToDisable = new List<Animator>();
        private static List<Animator> animatorsToDisableNow = new List<Animator>();

        // Call animator.Play instead of animator.SetTrigger, in case the animator is in transition.
        // Test case:  2015-11-15 Enter "SAT".  Type "RAT".  Expect R selected.  Got "R" resets to unselected.
        // http://answers.unity3d.com/questions/801875/mecanim-trigger-getting-stuck-in-true-state.html
        //
        // Do not call until initialized.  Test case:  2015-11-15 Got warning "Animator has not been initialized"
        // http://answers.unity3d.com/questions/878896/animator-has-not-been-initialized-1.html
        //
        // In editor, deleted and recreated animator state transition.  Test case:  2015-11-15 Got error "Transition '' in state 'selcted' uses parameter 'none' which is not compatible with condition type"
        // http://answers.unity3d.com/questions/1070010/transition-x-in-state-y-uses-parameter-z-which-is.html
        //
        // Unity expects not to animate the camera or the root itself.  Instead animate the child of the root.  The root might not move.
        // Test case:  2016-02-13 Animate camera position.  Play.  Camera does not move.  Generate root motion curves.  Apply root motion curves.  Still camera does not move.  Assign animator to parent of camera.  Animate child.  Then camera moves.
        //
        // isTrigger:  If true, then set trigger.  Otherwise play animation.
        public static void SetState(Animator animator, string state,
            bool isRestart = false, bool isTrigger = false)
        {
            if (null == animator)
            {
                DebugUtil.Log("AnimationView.SetState: Does animator exist? "
                    + SceneNodeView.GetPath(animator.gameObject)
                    + ": " + state);
                return;
            }
            if (!animator.isInitialized)
            {
                if (isVerbose)
                {
                    DebugUtil.Log("AnimationView.SetState: Animator is not initialized.");
                }
                return;
            }
            if (!animator.enabled)
            {
                animator.enabled = true;
            }
            if (isTrigger)
            {
                animator.SetTrigger(state);
            }
            else if (isRestart)
            {
                animator.Play(state, -1, 0f);
            }
            else
            {
                animator.Play(state);
            }
            bool isChange = !states.ContainsKey(animator)
                || states[animator] != state;
            if (isVerbose && (isRestart || isChange))
            {
                DebugUtil.Log("AnimationView.SetState: "
                    + SceneNodeView.GetPath(animator.gameObject)
                    + ": " + state + " at " + Time.time);
            }
            states[animator] = state;
            startTimes[state] = Time.time;
            MayAddToDisableOnEnd(animator);
        }

        public static void SetStates(List<Animator> animators, List<string> states)
        {
            for (int index = 0, end = states.Count; index < end; ++index)
            {
                SetState(animators[index], states[index]);
            }
        }

        // It would be higher performance to cache the animator.
        public static void SetState(GameObject animatorOwner, string state,
            bool isRestart = false, bool isTrigger = false)
        {
            Animator animator = animatorOwner.GetComponent<Animator>();
            SetState(animator, state, isRestart, isTrigger);
        }


        // It would be higher performance to cache the animators.
        public static void SetStates(List<GameObject> animatorOwners, List<string> states)
        {
            for (int index = 0, end = states.Count; index < end; ++index)
            {
                SetState(animatorOwners[index], states[index]);
            }
        }

        // Gotcha:
        // Trigger is not consumed until listening animation completes.
        // http://answers.unity3d.com/questions/801875/mecanim-trigger-getting-stuck-in-true-state.html
        // So if the trigger is in a state that doesn't listen to that trigger,
        // the trigger sticks around and could trigger as soon as it gets into a listening state.
        // That can make it appear as if the trigger never fired.
        // Playing the animation directly, avoids this latent trigger.
        // Also having the model recognize which states will trigger avoid a latent trigger.
        public static void SetTrigger(GameObject animatorOwner, string state)
        {
            SetState(animatorOwner, state, false, true);
        }

        // Does not support the animator transitioning to another state.
        // Depends on setting state to add animators to disable.
        // Disables on the next update.
        // Still, this function's calling order is fragile.
        // It works when updating disabling before any new animations are set.
        //
        // Remarks:
        // Useful to disable animation at the end of a clip.
        // Useful to speed up performance if animator update is a hotspot in the profiler.
        // Unfortunately Unity animations can be slow and take up processing time
        // even after the last keyframe.
        // Disabling all animators removes this cost from the profiler.
        // """Disable idle animations. Avoid design patterns where an animator sits in a loop setting a value to the same thing. There is considerable overhead for this technique, with no effect on the application. Instead, terminate the animation and restart when appropriate."""
        // https://developer.microsoft.com/en-us/windows/mixed-reality/performance_recommendations_for_unity
        //
        // I didn't find a more performant method in Unity to set an animation to disable on end.
        // An animation event sends a message, which is hundreds of times slower on that frame.
        // The update poll has overhead that scales with number of observed animators.
        // To reduce overhead, updating removes each animator when it disables.
        public static void UpdateDisableOnEnd(int layer = 0)
        {
            for (int index = animatorsToDisableNow.Count - 1; index >= 0; --index)
            {
                Animator animator = animatorsToDisableNow[index];
                animator.enabled = false;
            }
            animatorsToDisableNow.Clear();
            for (int index = animatorsToDisable.Count - 1; index >= 0; --index)
            {
                Animator animator = animatorsToDisable[index];
                if (animator == null || animator.gameObject == null
                    || !animator.isInitialized || !animator.enabled)
                {
                    animatorsToDisableNow.Add(animator);
                    animatorsToDisable.RemoveAt(index);
                    continue;
                }
                AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(layer);
                if (info.normalizedTime < 1.0f)
                {
                    continue;
                }
                animatorsToDisableNow.Add(animator);
                animatorsToDisable.RemoveAt(index);
            }
        }

        private static void MayAddToDisableOnEnd(Animator animator, int layer = 0)
        {
            if (!WillEnd(animator, layer))
            {
                return;
            }
            if (animatorsToDisable.IndexOf(animator) >= 0)
            {
                return;
            }
            animatorsToDisable.Add(animator);
        }

        // When animator is done and would clamp or play once, disables.
        private static bool WillEnd(Animator animator, int layer = 0)
        {
            AnimatorClipInfo[] clipInfos = animator.GetCurrentAnimatorClipInfo(layer);
            for (int index = 0, end = clipInfos.Length; index < end; ++index)
            {
                AnimationClip clip = clipInfos[index].clip;
                if (clip.wrapMode == WrapMode.Loop || clip.wrapMode == WrapMode.PingPong)
                {
                    return false;
                }
            }
            return true;
        }

        // Return name state that was completed now, or null.
        // Also erases that state, so next time this is called it won't be completed now.
        // Expects time passed since SetState to avoid race when CompletedNow is not called before SetState in the frame.
        // Expects animation is on layer 0.
        // Syncrhonous. Does not depend on event complete animation.
        //
        // http://answers.unity3d.com/questions/351534/how-to-get-current-state-on-mecanim.html
        //
        // Another way might be to get the currently playing animation.
        // http://answers.unity3d.com/questions/362629/how-can-i-check-if-an-animation-is-being-played-or.html
        //
        // Blubberfish says integers will compare faster than strings.
        // http://answers.unity3d.com/questions/407186/how-to-get-current-state-name-on-mecanim.html
        public static string CompletedNow(Animator animator, int layer = 0)
        {
            if (null == animator)
            {
                DebugUtil.Log("AnimationView.CompletedNow: Does animator exist? "
                    + SceneNodeView.GetPath(animator.gameObject));
                return null;
            }
            if (!animator.isInitialized)
            {
                return null;
            }
            if (!states.ContainsKey(animator))
            {
                return null;
            }
            string completedNow = states[animator];
            if (completedNow == null || startTimes[completedNow] == Time.time)
            {
                return null;
            }
            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(layer);
            if (info.normalizedTime < 1.0f)
            {
                return null;
            }
            states[animator] = null;
            if (isVerbose)
            {
                LogInfo(animator, completedNow, info, layer, "AnimationView.CompletedNow: ");
            }
            return completedNow;
        }

        public static string CompletedNow(GameObject animatorOwner, int layer = 0)
        {
            return CompletedNow(animatorOwner.GetComponent<Animator>(), layer);
        }

        // XXX Would be faster with StringBuilder, rather than string concatenation.
        private static void LogInfo(Animator animator, string completedNow, AnimatorStateInfo info,
            int layer, string prefix)
        {
            string message = prefix
                + SceneNodeView.GetPath(animator.gameObject)
                + ": " + completedNow + " at " + Time.time
                + " info " + info
                + " IsName " + info.IsName(completedNow)
                + " length " + info.length
                + " normalizedTime " + info.normalizedTime
            ;
            Animation animationStates = animator.GetComponent<Animation>();
            if (null != animationStates)
            {
                foreach (AnimationState state in animationStates)
                {
                    message += "\n    state " + state.name
                        + " time " + state.time
                        + " length " + state.length
                        + " weight " + state.weight
                        + " enabled " + state.enabled
                        + " normalizedTime " + state.normalizedTime
                    ;
                }
            }
            AnimatorClipInfo[] clipInfos = animator.GetCurrentAnimatorClipInfo(layer);
            for (int index = 0, end = clipInfos.Length; index < end; ++index)
            {
                AnimationClip clip = clipInfos[index].clip;
                message += "\n    clip " + clip.name + " length " + clip.length;
            }
            DebugUtil.Log(message);
        }
    }
}
