using UnityEngine;
using UnityEngine.UI;

namespace Finegamedesign.Utils
{
	public sealed class PauseView : MonoBehaviour
	{
		public Button pauseButton;
		public Button resumeButton;
		public Button quitButton;
		public Animator pauseAnimator;
		public PauseModel model = new PauseModel();

		private void Start()
		{
			pauseButton.onClick.AddListener(model.Pause);
			resumeButton.onClick.AddListener(model.Resume);
			if (quitButton == null)
			{
				return;
			}
			quitButton.onClick.AddListener(Quit);
		}

		private void Update()
		{
			AnimationView.SetState(pauseAnimator, model.GetState());
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
