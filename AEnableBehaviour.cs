using System;
using UnityEngine;

namespace Finegamedesign.Utils
{
    public abstract class AEnableBehaviour<T> : MonoBehaviour
        where T : AEnableBehaviour<T>
    {
        public static event Action<T> onEnable;
        public static event Action<T> onDisable;

        private void OnEnable()
        {
            if (onEnable == null)
            {
                return;
            }
            onEnable((T)this);
        }

        private void OnDisable()
        {
            if (onDisable == null)
            {
                return;
            }
            onDisable((T)this);
        }
    }
}
