using System;
using UnityEngine;

namespace Finegamedesign.Utils
{
    [Serializable]
    public sealed class PauseModel
    {
        private const string kNoneState = "none";
        private const string kBeginState = "begin";
        private const string kEndState = "end";

        public static event Action<string> onStateChanged;

        private string m_State = kNoneState;

        private bool m_IsPaused;

        public bool isPaused
        {
            get
            {
                return m_IsPaused;
            }
            set
            {
                if (value)
                {
                    Pause();
                }
                else
                {
                    Resume();
                }
                m_IsPaused = value;
            }
        }

        public string state
        {
            get
            {
                return m_State;
            }
            set
            {
                if (m_State == value)
                {
                    return;
                }
                m_State = value;
                if (onStateChanged == null)
                {
                    return;
                }
                onStateChanged(value);
            }
        }

        [SerializeField]
        [Tooltip("Scales time to zero when paused and back to one after resumed.")]
        private bool m_ScaleTime = false;

        public bool scaleTime
        {
            get { return m_ScaleTime; }
            set { m_ScaleTime = value; }
        }

        private float m_PreviousTimeScale = 1f;

        private bool m_IsVerbose = true;

        public void Pause()
        {
            if (m_IsPaused)
            {
                return;
            }
            m_IsPaused = true;
            if (m_ScaleTime)
            {
                m_PreviousTimeScale = Time.timeScale;
                if (m_PreviousTimeScale <= 0f)
                {
                    m_PreviousTimeScale = 1f;
                }
                if (m_IsVerbose)
                {
                    DebugUtil.Log("PauseModel.Pause: " + m_PreviousTimeScale + " time " + Time.timeScale);
                }
                Time.timeScale = 0f;
            }
            state = kBeginState;
        }

        public void Resume()
        {
            if (!m_IsPaused)
            {
                return;
            }
            m_IsPaused = false;
            if (m_ScaleTime)
            {
                if (m_PreviousTimeScale <= 0f)
                {
                    m_PreviousTimeScale = 1f;
                }
                if (m_IsVerbose)
                {
                    DebugUtil.Log("PauseModel.Resume: " + m_PreviousTimeScale + " time " + Time.timeScale);
                }
                Time.timeScale = m_PreviousTimeScale;
            }
            state = kEndState;
        }
    }
}
