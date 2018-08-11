using UnityEngine;  // Mathf
using System.Collections.Generic;  // List

namespace FineGameDesign.Utils
{
    public sealed class Deck
    {
        // Uniform distribution between -radius and +radius inclusive.
        public static int RandomRadius(int radius)
        {
            return UnityEngine.Random.Range(-radius, radius + 1);
        }

        // Unity Random docs says it includes 1.0, which would be out of range.
        public static float Random()
        {
            float value = UnityEngine.Random.value;
            for (int attempt = 0; value >= 1.0f && attempt < 256; ++attempt)
            {
                value = UnityEngine.Random.value;
            }
            if (value >= 1.0f)
            {
                value = 0.0f;
            }
            return value;
        }

        public static void ShuffleArray<T>(T[] deck)
        {
            for (int index = deck.Length - 1; 1 <= index; index--)
            {
                int r = (int) Mathf.Floor(Random() * (index + 1));
                T swap = deck[index];
                deck[index] = deck[r];
                deck[r] = swap;
            }
        }

        public static void ShuffleList<T>(List<T> deck)
        {
            for (int index = deck.Count - 1; 1 <= index; index--)
            {
                int r = (int) Mathf.Floor(Random() * (index + 1));
                T swap = deck[index];
                deck[index] = deck[r];
                deck[r] = swap;
            }
        }

        private int index = -1;
        private int length = -1;
        private float[] cards;

        public void Setup(float[] originals, int copies)
        {
            int original = originals.Length;
            length = copies * original;
            cards = new float[length];
            for (index = 0; index < length; index++)
            {
                cards[index] = originals[index % original];
            }
            ShuffleArray(cards);
            index = -1;
        }

        public float NextCard()
        {
            index++;
            if (length <= index) {
                ShuffleArray(cards);
                index = 0;
            }
            return cards[index];
        }
    }
}
