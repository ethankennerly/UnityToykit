using UnityEngine;

namespace FineGameDesign.Utils
{
    public sealed class TextureToByteArray2D : MonoBehaviour
    {
        [SerializeField]
        private Texture2D m_Texture;

        private ByteArray2D m_RedChannel;

        public ByteArray2D GetRedChannel()
        {
            if (m_RedChannel != null)
                return m_RedChannel;

            Color32[] colors = m_Texture.GetPixels32();
            int area = m_Texture.width * m_Texture.height;
            byte[] bytes = new byte[area];
            for (int index = 0; index < area; ++index)
            {
                bytes[index] = colors[index].r;
            }
            m_RedChannel = new ByteArray2D(bytes, m_Texture.width, m_Texture.height);
            return m_RedChannel;
        }
    }
}
