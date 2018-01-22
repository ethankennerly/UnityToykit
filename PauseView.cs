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

        [SerializeField]
        private PauseModel m_Model = new PauseModel();
        public PauseModel model
        {
            get
            {
                return m_Model;
            }
        }

        // Avoids case on WebGL.
        // Scene reloads but no interaction until pause and resume again.
        // No problem in Unity Editor WebGL.
        // But in browser, some clicks and keypresses are ignored.
        // Pausing and resuming works around this.
        private void Start()
        {
            m_Model.Resume();
        }

        private void OnDestroy()
        {
            m_Model.Resume();
        }

        private void OnEnable()
        {
            UpdateAnimation(model.state);
            PauseModel.onStateChanged += UpdateAnimation;
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
            PauseModel.onStateChanged -= UpdateAnimation;
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
