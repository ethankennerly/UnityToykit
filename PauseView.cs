using UnityEngine;
using UnityEngine.UI;

namespace Finegamedesign.Utils
{
	public sealed class PauseView : MonoBehaviour
	{
		public Button pauseButton;
		public Button resumeButton;
		public Animator pauseAnimator;
		public PauseModel model = new PauseModel();

		private void Start()
		{
			pauseButton.onClick.AddListener(model.Pause);
			resumeButton.onClick.AddListener(model.Resume);
		}

		private void Update()
		{
			AnimationView.SetState(pauseAnimator, model.GetState());
		}
	}
}
