using UnityEngine;  // Mathf
using System;  // Random
using System.Collections.Generic;  // List

public class Deck
{
	private static System.Random rng = new System.Random();

	public static float Random()
	{
		return (float) rng.NextDouble();
	}

	/**
	 * Unity Random includes 1.0, which would be out of range.
	 */
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

	/**
	 * Unity Random includes 1.0, which would be out of range.
	 */
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

	// Pop interpolated card from 0 to 1.
	// Example:  Editor/Tests/TestDeck.cs
	public static T Progress<T>(List<T> deck, float normal)
    {
		int index = (int)Mathf.Floor(normal * deck.Count);
		index = Mathf.Min(deck.Count - 1, index);
		index = Mathf.Max(0, index);
		T card = deck[index];
		deck.RemoveAt(index);
		return card;
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
