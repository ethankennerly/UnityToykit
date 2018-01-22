using System.Collections.Generic;
using UnityEngine;

namespace Finegamedesign.Utils
{
    public sealed class TimerView : MonoBehaviour
    {
        [SerializeField]
        private Timer m_Model;
        public Timer model
        {
            get
            {
                return m_Model;
            }
            set
            {
                if (m_Model == value)
                {
                    return;
                }
                RemoveListeners(m_Model);
                m_Model = value;
                AddListeners(value);
            }
        }
        public string state = "begin";
        public bool isChangeState = false;
        public bool isSyncNormal = true;
        // Helpful for debugging.
        private float m_Normal = -1.0f;

        public List<Animator> animators;

        public Animator stateNormalAnimator;

        public static TimerView[] Binds(Timer model, TimerView[] timers)
        {
            if (timers == null || timers.Length == 0)
            {
                timers = FindObjectsOfType<TimerView>();
            }
            for (int index = 0, end = timers.Length; index < end; ++index)
            {
                TimerView view = timers[index];
                view.model = model;
            }
            return timers;
        }

        private void OnEnable()
        {
            SetupAnimators();
            AddListeners(model);
        }

        private void AddListeners(Timer model)
        {
            if (model == null)
            {
                return;
            }
            RemoveListeners(model);
            if (isChangeState)
            {
                OnStateChanged(model.State.value);
                model.State.onChanged += OnStateChanged;
            }
            else
            {
                model.State.onChanged += OnStateNormalChanged;
                OnStateChanged(state);
            }
            if (isSyncNormal)
            {
                OnNormalChanged(model.normal.value);
                model.normal.onChanged += OnNormalChanged;
            }
        }

        private void OnDisable()
        {
            RemoveListeners(model);
        }

        private void RemoveListeners(Timer model)
        {
            if (model == null)
            {
                return;
            }
            if (isChangeState)
            {
                model.State.onChanged -= OnStateChanged;
            }
            if (isSyncNormal)
            {
                model.normal.onChanged -= OnNormalChanged;
            }
        }

        private void SetupAnimators()
        {
            if (animators == null)
            {
                animators = new List<Animator>();
            }
            if (animators.Count == 0)
            {
                animators.Add(GetComponent<Animator>());
            }
        }

        private void OnStateNormalChanged(string nextState)
        {
            if (stateNormalAnimator == null)
            {
                return;
            }
            stateNormalAnimator.Play(nextState);
        }

        private void OnStateChanged(string nextState)
        {
            state = nextState;
            float nextNormal = isSyncNormal ? m_Normal : 0.0f;
            GotoNormal(nextNormal);
        }

        private void OnNormalChanged(float nextNormal)
        {
            m_Normal = nextNormal;
            if (!isSyncNormal)
            {
                return;
            }
            GotoNormal(nextNormal);
        }

        private void GotoNormal(float normal)
        {
            for (int index = 0, end = animators.Count; index < end; ++index)
            {
                Animator animator = animators[index];
                animator.Play(state, -1, normal);
                animator.speed = 0.0f;
            }
        }
    }
}
