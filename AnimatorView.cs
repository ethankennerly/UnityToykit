using System.Collections.Generic;
using UnityEngine;

namespace Finegamedesign.Utils
{
    [RequireComponent(typeof(Animator))]
    public sealed class AnimatorView : MonoBehaviour
    {
        private Animator m_Animator;

        private string m_OnEnd = "DisableAnimator";

        private void Start()
        {
            Setup();
        }

        // Might be called
        public void Setup()
        {
            if (m_Animator != null)
            {
                return;
            }
            m_Animator = (Animator)GetComponent(typeof(Animator));
            OnEnd(m_Animator, m_OnEnd);
        }

        // Usage:
        // Attach this script to the object with the animator.
        // Add animation event at end of the animation clip.
        // Configure the animation event to call DisableAnimator.
        // Unity AnimationEvent can only call a function on the animator object.
        // The animator needs to be re-enabled before it will play.
        // For example, AnimationView.SetState re-enables an animator.
        // AnimationEvent uses SendMessage, which is over 100x times slower than a function call!
        // https://answers.unity.com/questions/243876/efficiency-of-sendmessage.html
        //
        // Remarks:
        // Useful to disable animation at the end of a clip.
        // Useful to speed up performance if animator update is a hotspot in the profiler.
        // Unfortunately Unity animations can be slow and take up processing time
        // even after the last keyframe.
        // Disabling all animators removes this cost from the profiler.
        // """Disable idle animations. Avoid design patterns where an animator sits in a loop setting a value to the same thing. There is considerable overhead for this technique, with no effect on the application. Instead, terminate the animation and restart when appropriate."""
        // https://developer.microsoft.com/en-us/windows/mixed-reality/performance_recommendations_for_unity
        public void DisableAnimator()
        {
            m_Animator.enabled = false;
        }

        // Animation clips might be shared by many objects.
        // If the clip was already visited, ignores that clip.
        // Optimization depends on no clips being added or having the same function name.
        // If one clip has this function name, then exits.
        private void OnEnd(Animator animator, string functionName)
        {
            AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
            for (int index = 0, end = clips.Length; index < end; ++index)
            {
                AnimationClip clip = clips[index];
                AnimationEvent[] events = clip.events;
                bool isNew = true;
                for (int e = 0, eEnd = events.Length; e < eEnd; ++e)
                {
                    AnimationEvent thatEvent = events[e];
                    if (thatEvent.functionName == functionName
                        && thatEvent.time == clip.length)
                    {
                        isNew = false;
                        break;
                    }
                }
                if (!isNew)
                {
                    continue;
                }
                AnimationEvent onEnd = new AnimationEvent();
                onEnd.time = clip.length;
                onEnd.functionName = functionName;
                clip.AddEvent(onEnd);
            }
        }
    }
}
