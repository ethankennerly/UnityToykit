using System;

namespace FineGameDesign.Utils
{
    /// <summary>
    /// Only updates when not paused.
    /// </summary>
    public sealed class HorizontalSteeringSystemView : ASingletonView<HorizontalSteeringSystem>
    {
        private Action<float> m_OnDeltaTime;

        private Action<float, float> m_OnKeyXY;

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
                m_OnKeyXY = controller.SteerXY;
            KeyInputSystem.instance.onKeyXY -= m_OnKeyXY;
            KeyInputSystem.instance.onKeyXY += m_OnKeyXY;
            ClickInputSystem.instance.onAxisXY -= m_OnKeyXY;
            ClickInputSystem.instance.onAxisXY += m_OnKeyXY;
        }

        private void RemoveListeners()
        {
            PauseSystem.onDeltaTime -= m_OnDeltaTime;
            KeyInputSystem.instance.onKeyXY -= m_OnKeyXY;
            ClickInputSystem.instance.onAxisXY -= m_OnKeyXY;
        }
    }
}
