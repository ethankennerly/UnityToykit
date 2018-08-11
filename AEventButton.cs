using System;
using UnityEngine;
using UnityEngine.UI;

namespace FineGameDesign.Utils
{
    [RequireComponent(typeof(Button))]
    public abstract class AEventButton<T> : MonoBehaviour
        where T : AEventButton<T>
    {
        public static event Action<T> onClick;

        private Button m_Button;

        private void OnEnable()
        {
            m_Button = GetComponent<Button>();
            m_Button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            m_Button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            if (!ClickInputSystem.instance.DisableTemporarily())
            {
                return;
            }
            if (onClick == null)
            {
                return;
            }
            onClick((T)this);
        }
    }
}
