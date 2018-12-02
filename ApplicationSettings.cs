using UnityEngine;

namespace FineGameDesign.Utils
{
    public sealed class ApplicationSettings : MonoBehaviour
    {
        [SerializeField]
        #pragma warning disable 0414
        private int m_TargetFrameRate = 30;
        #pragma warning restore 0414

        private void OnEnable()
        {
            SetFrameRate();
        }

        /// <summary>
        /// WebGL build ignores this.
        /// Otherwise JavaScript console error:
        ///     "Looks like you are rendering without using requestAnimationFrame for the main loop. You should use 0 for the frame rate in emscripten_set_main_loop in order to use requestAnimationFrame, as that can greatly improve your frame rates!"
        /// <a href="https://forum.unity.com/threads/rendering-without-using-requestanimationframe-for-the-main-loop.373331/">Rendering without using requestAnimationFrame</a>
        /// </summary>
        private void SetFrameRate()
        {
            #if !UNITY_WEBGL
            Application.targetFrameRate = m_TargetFrameRate;
            #endif
        }
    }
}
