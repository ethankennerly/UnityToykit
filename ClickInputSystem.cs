using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FineGameDesign.Utils
{
    /// <summary>
    /// Serializes to log what is clicked and configure in the editor.
    /// </summary>
    [Serializable]
    public sealed class ClickInputSystem : ASingleton<ClickInputSystem>
    {
        public event Action<Vector3> onWorld;
        public event Action<float, float> onWorldXY;
        public event Action<float, float> onViewportXY;
        public event Action<float, float> onAxisDownXY;
        public event Action<float, float> onAxisXY;

        public event Action<Vector3> onCollisionPoint;
        public event Action<Vector2> onCollisionPoint2D;
        public event Action<Collider> onCollisionEnter;
        public event Action<Collider2D> onCollisionEnter2D;

        [SerializeField]
        private float m_DisabledDuration = 0.25f;
        public float disabledDuration
        {
            get { return m_DisabledDuration; }
            set { m_DisabledDuration = value; }
        }
        [NonSerialized]
        private float m_UpdateTime = -1.0f;
        [NonSerialized]
        private float m_ClickTime = -1.0f;

        [NonSerialized]
        private RaycastHit m_Hit;

        [NonSerialized]
        private Vector2 m_OverlapPoint = new Vector2();
        [NonSerialized]
        private Vector3 m_CollisionPoint = new Vector3();
        [NonSerialized]
        private Vector3 m_World = new Vector3();
        [NonSerialized]
        private Vector3 m_Viewport = new Vector3();
        [NonSerialized]
        private Vector2 m_Axis = new Vector2();
        private Vector2 m_AxisDown = new Vector2();

        [SerializeField]
        private Camera m_Camera;

        [SerializeField]
        private bool m_IsVerbose = false;

        /// <summary>
        /// Caches time to ignore multiple calls per frame.
        ///
        /// Ignores if over UI object if there is an event system.
        /// Otherwise, a tap on a button is also reacted as a tap in viewport.
        /// For example, in Deadly Diver, a viewport tap moves the diver.
        ///
        /// Caches camera.  Unfortunately Unity does not cache the main camera.
        /// <summary>
        public void Update()
        {
            float time = Time.time;
            if (m_UpdateTime == time)
            {
                return;
            }
            m_UpdateTime = time;

            if (m_Camera == null)
            {
                m_Camera = Camera.main;
            }

            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                if (m_IsVerbose)
                {
                    DebugUtil.Log("ClickInputSystem.Update: Pointer is over a UI object. Ignoring world objects.");
                }
                return;
            }

            if (Input.GetMouseButton(0))
            {
                Axis();
            }

            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }

            m_World = m_Camera.ScreenToWorldPoint(Input.mousePosition);
            m_OverlapPoint.x = m_World.x;
            m_OverlapPoint.y = m_World.y;

            Raycast();
            Raycast2D();
            OverlapPoint();
            Screen();
            ViewportDown();
        }

        /// <returns>
        /// If currently enabled, else already disabled.
        /// Because of click-point update racing between other updates,
        /// each caller disables individually.
        /// </returns>
        public bool DisableTemporarily()
        {
            bool enabled = m_ClickTime < 0f;
            float time = Time.time;
            if (!enabled)
            {
                float enabledTime = m_ClickTime + m_DisabledDuration;
                enabled = time >= enabledTime;
            }
            if (enabled)
            {
                m_ClickTime = time;
            }
            if (m_IsVerbose)
            {
                DebugUtil.Log("ClickInputSystem.enabled: " + enabled
                    + " since " + m_ClickTime + " for " + m_DisabledDuration);
            }
            return enabled;
        }

        private bool Raycast()
        {
            if (!Physics.Raycast(m_Camera.ScreenPointToRay(Input.mousePosition), out m_Hit))
            {
                return false;
            }
            m_CollisionPoint = m_Hit.point;
            if (m_IsVerbose)
            {
                DebugUtil.Log("ClickInputSystem.RayCast: " + m_CollisionPoint);
            }
            if (onCollisionPoint != null)
            {
                onCollisionPoint(m_CollisionPoint);
            }
            if (onCollisionEnter != null)
            {
                onCollisionEnter(m_Hit.collider);
            }
            return true;
        }

        private bool Raycast2D()
        {
            RaycastHit2D hit = Physics2D.Raycast(m_OverlapPoint, Vector2.zero);
            if (hit == null || hit.collider == null)
            {
                return false;
            }
            if (m_IsVerbose)
            {
                DebugUtil.Log("ClickInputSystem.Raycast2D: " + hit.collider);
            }
            if (onCollisionEnter2D != null)
            {
                onCollisionEnter2D(hit.collider);
            }
            return true;
        }

        private bool OverlapPoint()
        {
            if (Physics2D.OverlapPoint(m_OverlapPoint) == null)
            {
                return false;
            }
            if (onCollisionPoint2D != null)
            {
                onCollisionPoint2D(m_OverlapPoint);
            }
            if (m_IsVerbose)
            {
                DebugUtil.Log("ClickInputSystem.OverlapPoint: " + m_OverlapPoint);
            }
            return true;
        }

        private bool Axis()
        {
            m_Viewport = m_Camera.ScreenToViewportPoint(Input.mousePosition);
            if (onAxisXY != null)
            {
                m_Axis.x = m_Viewport.x - 0.5f;
                m_Axis.y = m_Viewport.y - 0.5f;
                m_Axis.Normalize();
                onAxisXY(m_Axis.x, m_Axis.y);
            }
            if (m_IsVerbose)
            {
                DebugUtil.Log("ClickInputSystem.Axis: " + m_Viewport);
            }
            return true;
        }

        private bool ViewportDown()
        {
            m_Viewport = m_Camera.ScreenToViewportPoint(Input.mousePosition);
            if (onViewportXY != null)
            {
                onViewportXY(m_Viewport.x, m_Viewport.y);
            }
            if (onAxisDownXY != null)
            {
                m_AxisDown.x = m_Viewport.x - 0.5f;
                m_AxisDown.y = m_Viewport.y - 0.5f;
                m_AxisDown.Normalize();
                onAxisXY(m_AxisDown.x, m_AxisDown.y);
            }
            if (m_IsVerbose)
            {
                DebugUtil.Log("ClickInputSystem.ViewportDown: " + m_Viewport);
            }
            return true;
        }

        private bool Screen()
        {
            if (onWorld != null)
            {
                onWorld(m_World);
            }
            if (onWorldXY != null)
            {
                onWorldXY(m_World.x, m_World.y);
            }
            if (m_IsVerbose)
            {
                DebugUtil.Log("ClickInputSystem.Screen: " + m_World);
            }
            return true;
        }
    }
}
