using UnityEngine;

namespace FineGameDesign.Utils
{
    public sealed class OnEnableAudioPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioSource m_Source;

        [SerializeField]
        private AudioClip[] m_Clips;

        private void OnEnable()
        {
            AudioUtils.PlayRandom(m_Source, m_Clips);
        }
    }
}
