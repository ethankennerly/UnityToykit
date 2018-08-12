using System;
using UnityEngine;

namespace FineGameDesign.Utils
{
    /// <summary>
    /// Only updates when not paused.
    /// </summary>
    public sealed class HorizontalSteeringSystemView : ASingletonView<HorizontalSteeringSystem>
    {
        private Action<float> m_OnDeltaTime;

        private Action<float, float> m_OnKeyXY;
        private Action<float, float> m_OnKeyDownXY;

        [Header("Optional")]
        [SerializeField]
        private AudioSource m_AudioSource;

        [Header("Optional")]
        [SerializeField]
        private AudioClip m_LeftClip;

        [Header("Optional")]
        [SerializeField]
        private AudioClip m_RightClip;

        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void AddListeners()
        {
            if (m_OnDeltaTime == null)
                m_OnDeltaTime = controller.Update;
            PauseSystem.onDeltaTime -= m_OnDeltaTime;
            PauseSystem.onDeltaTime += m_OnDeltaTime;

            if (m_OnKeyXY == null)
                m_OnKeyXY = SteerXY;
            KeyInputSystem.instance.onKeyXY -= m_OnKeyXY;
            KeyInputSystem.instance.onKeyXY += m_OnKeyXY;
            ClickInputSystem.instance.onAxisXY -= m_OnKeyXY;
            ClickInputSystem.instance.onAxisXY += m_OnKeyXY;

            if (m_OnKeyDownXY == null)
                m_OnKeyDownXY = TryPlaySteeringSoundXY;
            KeyInputSystem.instance.onKeyDownXY -= m_OnKeyDownXY;
            KeyInputSystem.instance.onKeyDownXY += m_OnKeyDownXY;
            ClickInputSystem.instance.onAxisDownXY -= m_OnKeyDownXY;
            ClickInputSystem.instance.onAxisDownXY += m_OnKeyDownXY;
        }

        private void RemoveListeners()
        {
            PauseSystem.onDeltaTime -= m_OnDeltaTime;
            KeyInputSystem.instance.onKeyXY -= m_OnKeyXY;
            ClickInputSystem.instance.onAxisXY -= m_OnKeyXY;
            ClickInputSystem.instance.onAxisDownXY -= m_OnKeyDownXY;
        }

        private void SteerXY(float x, float y)
        {
            controller.SteerXY(x, y);
        }

        private void TryPlaySteeringSoundXY(float x, float y)
        {
            if (!controller.WouldSteer(x))
                return;

            if (m_AudioSource == null)
                return;

            AudioClip direction = x < 0 ? m_LeftClip : m_RightClip;
            if (direction == null)
                return;

            m_AudioSource.PlayOneShot(direction);
        }
    }
}
