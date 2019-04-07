using System;
using UnityEngine;
using UnityEngine.UI;

namespace FineGameDesign.Utils
{
    public sealed class PauseSystemView : ASingletonView<PauseSystem>
    {
        [Serializable]
        public struct StateNameLink
        {
            public PauseSystem.State state;
            public string name;
        }

        [SerializeField]
        private Button[] m_PauseButtons;

        [SerializeField]
        private Button[] m_ResumeButtons;

        [Header("Optional. Quits application.")]
        [SerializeField]
        private Button m_QuitButton;

        [Header("Optional: Plays linked animation name.")]
        [SerializeField]
        private Animator m_PauseAnimator;

        [SerializeField]
        private StateNameLink[] m_AnimationLinks;

        /// <summary>
        /// On enable, resets.
        /// Otherwise, if set paused in editor,
        /// in play mode pause overlay did not appear.
        /// </summary>
        private void OnEnable()
        {
            controller.Reset();
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void AddListeners()
        {
            UpdateAnimation(controller.state);
            PauseSystem.onStateChanged += UpdateAnimation;
            foreach (var button in m_PauseButtons)
                button.onClick.AddListener(controller.Pause);
            foreach (var button in m_ResumeButtons)
                button.onClick.AddListener(controller.Resume);
            if (m_QuitButton != null)
                m_QuitButton.onClick.AddListener(Quit);
        }

        private void RemoveListeners()
        {
            PauseSystem.onStateChanged -= UpdateAnimation;
            foreach (var button in m_PauseButtons)
                button.onClick.RemoveListener(controller.Pause);
            foreach (var button in m_ResumeButtons)
                button.onClick.RemoveListener(controller.Resume);
            if (m_QuitButton != null)
                m_QuitButton.onClick.RemoveListener(Quit);
        }

        private void Update()
        {
            controller.Update(Time.deltaTime);
        }

        private void UpdateAnimation(PauseSystem.State state)
        {
            if (m_PauseAnimator == null)
            {
                return;
            }

            foreach (StateNameLink link in m_AnimationLinks)
            {
                if (link.state != state)
                    continue;

                AnimationView.SetState(m_PauseAnimator, link.name);
                break;
            }
        }

        private void Quit()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
