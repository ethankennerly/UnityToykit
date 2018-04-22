using UnityEngine;

namespace Finegamedesign.Utils
{
    public static class AudioUtils
    {
        public static void PlayRandom(AudioSource source, AudioClip[] clips)
        {
            if (source == null || clips == null || clips.Length == 0)
            {
                return;
            }
            source.clip = ChooseRandom(clips);
            source.Play();
        }

        private static T ChooseRandom<T>(T[] elements)
        {
            int index = Random.Range(0, elements.Length);
            return elements[index];
        }
    }
}
