using UnityEngine;

namespace FineGameDesign.Utils
{
    public sealed class ApplicationSettings : MonoBehaviour
    {
        [SerializeField]
        private int m_TargetFrameRate = 30;

        public void OnEnable()
        {
            Application.targetFrameRate = m_TargetFrameRate;
        }
    }
}
