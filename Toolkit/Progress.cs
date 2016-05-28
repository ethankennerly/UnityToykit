using UnityEngine/*<Mathf>*/;
using System.Collections.Generic;
using System.Collections;

public class Progress
{
	public int level = 0;
	public int levelMax = 0;
	public float normal = 0.0f;
	public float radius = 
			0.03125f;
			// 0.0625f;
			// 0.1f;
	private ArrayList cardsOriginally;

	// Pop interpolated card from 0 to 1.
	// Example:  Editor/Tests/TestDeck.cs
	public T Pop<T>(List<T> cards)
    {
		int index = (int)Mathf.Floor(normal * cards.Count);
		index = Mathf.Min(cards.Count - 1, index);
		index = Mathf.Max(0, index);
		T card = cards[index];
		if (null == cardsOriginally) {
			cardsOriginally = new ArrayList(cards);
			levelMax = cards.Count;
		}
		level = cardsOriginally.IndexOf(card) + 1;
		cards.RemoveAt(index);
		return card;
    }

	// Midway is no change.
	// Example:  Editor/Tests/TestProgress.cs
	public float Creep(float performance, float performanceMax)
	{
		float change = (
			((float)performance / performanceMax) - 0.5f) 
			* 2.0f;
		change = Mathf.Max(-1.0f, Mathf.Min(1.0f, change));
		float progress = change * radius;
		float remaining = 1.0f - normal;
		progress *= remaining;
		normal += progress;
		normal = Mathf.Max(0.0f, Mathf.Min(1.0f, normal));
		Debug.Log("Progress.creep: progress " + progress + " normal " + normal + " performance " + performance + " change " + change);
		return normal;
	}

	public int up(float performance, float performanceMax)
	{
		float normal = (float)((performance / performanceMax) - 0.5f) * 2.0f;
		normal = Mathf.Max(-1.0f, Mathf.Min(1.0f, normal));
		float progress = normal * radius;
		float remaining = ((float)levelMax - level) / levelMax;
		progress *= remaining;
		int add = (int)(progress * levelMax);
		add = Mathf.Max(1, add);
		Debug.Log("Progress.up: progress " + progress + " normal " + normal + " performance " + performance);
		return add;
	}
}
