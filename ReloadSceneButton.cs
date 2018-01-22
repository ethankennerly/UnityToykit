using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Finegamedesign.Utils
{
    [RequireComponent(typeof(Button))]
    public sealed class ReloadSceneButton : MonoBehaviour
    {
        private Button m_Button;

        private void OnEnable()
        {
            if (m_Button == null)
            {
                m_Button = (Button)GetComponent(typeof(Button));
            }
            m_Button.onClick.AddListener(ReloadScene);
        }

        private void OnDisable()
        {
            m_Button.onClick.RemoveListener(ReloadScene);
        }

        private void ReloadScene()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
        }
    }
}
