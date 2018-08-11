using System;
using UnityEngine;

namespace FineGameDesign.Utils
{
    /// <summary>
    /// 1. [x] Horizontal Steering System
    ///     1. [x] X Axis Dead Zone.
    ///         1. [x] Listens to Key Input System On Key Down X Y.
    ///         1. [x] Listens to Click System On Axis X Y.
    ///         1. [x] If X in Dead Zone, stop here.
    ///         1. [x] Publish On Steer X.
    ///     1. [x] Reset X Time
    ///         1. [x] Afterward reset X to zero.
    ///         1. [x] Listens to pause system delta time.
    /// </summary>
    [Serializable]
    public sealed class HorizontalSteeringSystem : ASingleton<HorizontalSteeringSystem>
    {
        public static event Action<float> onSteerX;

        [SerializeField]
        private float m_XAxisMin = 0.25f;

        [SerializeField]
        private float m_XMultiplier = -1f;

        [SerializeField]
        private float m_SteeringDuration = 0.125f;

        [NonSerialized]
        private float m_SteeringTimeRemaining = 0f;

        [NonSerialized]
        private bool m_IsSteering = false;

        public void Update(float deltaTime)
        {
            UpdateStopSteering(deltaTime);
        }

        public void SteerXY(float x, float y)
        {
            SteerX(x);
        }

        private void UpdateStopSteering(float deltaTime)
        {
            if (!m_IsSteering)
                return;

            m_SteeringTimeRemaining -= deltaTime;
            if (m_SteeringTimeRemaining > 0f)
                return;

            m_IsSteering = false;
            if (onSteerX == null)
                return;

            onSteerX(0f);
        }

        private void SteerX(float x)
        {
            if (x < m_XAxisMin && x >= -m_XAxisMin)
                return;

            if (onSteerX == null)
                return;

            m_IsSteering = true;
            m_SteeringTimeRemaining = m_SteeringDuration;

            float steerAmount = m_XMultiplier * x;
            if (onSteerX == null)
                return;

            onSteerX(steerAmount);
        }
    }
}
