using UnityEngine;

namespace Finegamedesign.Utils
{
    public class ASingletonView<T> : MonoBehaviour
        where T : new()
    {
        [SerializeField]
        private T m_Controller = ASingleton<T>.instance;

        public T Controller
        {
            get { return m_Controller; }
        }
    }
}
