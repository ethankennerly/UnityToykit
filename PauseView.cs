using UnityEngine;
using UnityEngine.UI;

namespace Finegamedesign.Utils
{
    public sealed class PauseView : MonoBehaviour
    {
        public Button pauseButton;
        public Button resumeButton;
        [Tooltip("Optional. Quits application.")]
        public Button quitButton;
        [Tooltip("Animates when paused, by state names 'none' (default), 'begin' (pause), and 'end' (pause).")]
        public Animator pauseAnimator;

        private PauseModel m_Model = new PauseModel();
        public PauseModel model
        {
            get
            {
                return m_Model;
            }
        }

        private void OnEnable()
        {
            UpdateAnimation(model.state);
            model.onStateChanged += UpdateAnimation;
            pauseButton.onClick.AddListener(model.Pause);
            resumeButton.onClick.AddListener(model.Resume);
            if (quitButton == null)
            {
                return;
            }
            quitButton.onClick.AddListener(Quit);
        }

        private void OnDisable()
        {
            model.onStateChanged -= UpdateAnimation;
            pauseButton.onClick.RemoveListener(model.Pause);
            resumeButton.onClick.RemoveListener(model.Resume);
            if (quitButton == null)
            {
                return;
            }
            quitButton.onClick.RemoveListener(Quit);
        }

        private void UpdateAnimation(string state)
        {
            AnimationView.SetState(pauseAnimator, state);
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
