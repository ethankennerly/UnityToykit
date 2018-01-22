using System;

namespace Finegamedesign.Utils
{
    public sealed class PauseModel
    {
        private const string kNoneState = "none";
        private const string kBeginState = "begin";
        private const string kEndState = "end";

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

        public event Action<string> onStateChanged;

        public void Pause()
        {
            if (m_IsPaused)
            {
                return;
            }
            m_IsPaused = true;
            state = kBeginState;
        }

        public void Resume()
        {
            if (!m_IsPaused)
            {
                return;
            }
            m_IsPaused = false;
            state = kEndState;
        }
    }
}
