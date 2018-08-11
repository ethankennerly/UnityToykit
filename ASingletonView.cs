using UnityEngine;

namespace FineGameDesign.Utils
{
    public class ASingletonView<T> : MonoBehaviour
        where T : new()
    {
        [SerializeField]
        private T m_Controller = ASingleton<T>.instance;

        public T controller
        {
            get { return m_Controller; }
        }
    }
}
