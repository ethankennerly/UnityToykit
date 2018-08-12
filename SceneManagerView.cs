using UnityEngine;
using UnityEngine.SceneManagement;

namespace FineGameDesign.Utils
{
    /// <summary>
    /// Convenient to wire to Unity events in Unity editor.
    /// </summary>
    public sealed class SceneManagerView : MonoBehaviour
    {
        public void LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void LoadFirstScene()
        {
            LoadScene(0);
        }

        public void ReloadActiveScene()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            LoadScene(sceneName);
        }
    }
}
