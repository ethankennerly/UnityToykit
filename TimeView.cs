using UnityEngine;

namespace Finegamedesign.Utils
{
    public sealed class TimeView : MonoBehaviour
    {
        [SerializeField]
        [Range(0f, 8f)]
        private float m_TimeScale = 1.0f;

        [SerializeField]
        private float m_RoundToNearest = 0.25f;

        private void Start()
        {
            m_TimeScale = Time.timeScale;
        }

        private void OnValidate()
        {
            if (m_TimeScale == Time.timeScale)
            {
                return;
            }
            m_TimeScale = Mathf.Round(m_TimeScale / m_RoundToNearest)
                * m_RoundToNearest;
            Time.timeScale = m_TimeScale;
            m_TimeScale = Time.timeScale;
        }

        private void Update()
        {
            if (m_TimeScale == Time.timeScale)
            {
                return;
            }
            m_TimeScale = Time.timeScale;
        }
    }
}
