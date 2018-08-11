using System;
using UnityEngine;

namespace FineGameDesign.Utils
{
    [Serializable]
    public sealed class PauseSystem : ASingleton<PauseSystem>
    {
        public enum State
        {
            None,
            Begin,
            End
        }

        public static event Action<State> onStateChanged;

        public static event Action<float> onDeltaTime;

        private State m_State = State.None;

        [SerializeField]
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

        public State state
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

        [NonSerialized]
        private float m_DeltaTime = 0f;
        public float deltaTime
        {
            get { return m_DeltaTime; }
            set { m_DeltaTime = value; }
        }

        private float m_PreviousTimeScale = 1f;

        private bool m_IsVerbose = true;

        /// <summary>
        /// Avoids case on WebGL.
        /// Scene reloads but no interaction until pause and resume again.
        /// No problem in Unity Editor WebGL.
        /// But in browser, some clicks and keypresses are ignored.
        /// Pausing and resuming works around this.
        /// </summary>
        public void ResetWebGL()
        {
            if (m_IsPaused)
            {
                Resume();
                Pause();
            }
            else
            {
                Resume();
            }
        }

        public void Update(float deltaTime)
        {
            if (m_IsPaused)
            {
                deltaTime = 0f;
            }
            m_DeltaTime = deltaTime;

            if (deltaTime == 0f)
                return;

            if (onDeltaTime == null)
                return;

            onDeltaTime(deltaTime);
        }

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
                    DebugUtil.Log("PauseSystem.Pause: " + m_PreviousTimeScale + " time " + Time.timeScale);
                }
                Time.timeScale = 0f;
            }
            state = State.Begin;
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
                    DebugUtil.Log("PauseSystem.Resume: " + m_PreviousTimeScale + " time " + Time.timeScale);
                }
                Time.timeScale = m_PreviousTimeScale;
            }
            state = State.End;
        }
    }
}
