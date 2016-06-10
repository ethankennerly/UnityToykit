using UnityEngine/*<Mathf>*/;
using System.Collections.Generic;
using System.Collections;


// Requirements:
// 
// 	May go down in level.
// 	Do not repeat levels.  Once a level was repeated.
// 
// System:
// 
// 	Adjust normalized level range.
// 	Deck of cards.
// 		Draw from deck at normalized position.
// 			Original index.
// 			Closest index in remaining.
// 		Identify original index.
//
// Test case:  2016-05-21 Anagram Attack.  Very high level.
// Jennifer Russ expects to not feel overwhelmed.
// Jennifer Russ expects to not repeat a word.
public class Progress
{
	public int level = 0;
	public int levelNormalMax = 1000;
	public int levelMax = 0;
	public float normal = 0.0f;
	public float radius = 
			0.03125f;
			// 0.0625f;
			// 0.1f;
	private ArrayList cardsOriginally;
	public bool isVerbose = false;

	public bool isCheckpoint = false;
	public float checkpointStep = -1.0f;
	public float checkpoint = -1.0f;

	// Pop interpolated card from 0 to 1.
	// Example:  Editor/Tests/TestProgress.cs
	public T Pop<T>(List<T> cards, int min = 0)
    {
		int index = (int)Mathf.Floor(normal * cards.Count);
		index = Mathf.Min(cards.Count - 1, index);
		index = Mathf.Max(min, index);
		T card = cards[index];
		if (null == cardsOriginally) {
			cardsOriginally = new ArrayList(cards);
			levelMax = cards.Count;
		}
		level = cardsOriginally.IndexOf(card) + 1;
		cards.RemoveAt(index);
		return card;
    }

	public float NextCreep(float performanceNormal)
	{
		float change = (performanceNormal - 0.5f) * 2.0f;
		change = Mathf.Max(-1.0f, Mathf.Min(1.0f, change));
		float progress = change * radius;
		float remaining = 1.0f - normal;
		progress *= remaining;
		float next = normal + progress;
		next = Mathf.Max(0.0f, Mathf.Min(1.0f, next));
		return next;
	}

	// Midway is no change.
	// Example:  Editor/Tests/TestProgress.cs
	public float Creep(float performanceNormal)
	{
		normal = NextCreep(performanceNormal);
		normal = UpdateCheckpoint(normal);
		if (isVerbose) {
			Debug.Log("Progress.creep: normal " + normal + " performance " + performanceNormal);
		}
		return normal;
	}

	/**
	 * At each step of progress, clamp to checkpoint.
	 * Example:  Editor/Tests/TestProgress.cs
	 */
	public void SetCheckpointStep(float step)
	{
		checkpointStep = step;
		checkpoint = Mathf.Floor(normal / checkpointStep + 1) * checkpointStep;
	}

	/**
	 * Discards excess progress after checkpoint.
	 * Example:  Editor/Tests/TestProgress.cs
	 */
	public float UpdateCheckpoint(float normal)
	{
		isCheckpoint = 0.0f <= checkpoint && checkpoint <= normal;
		if (isCheckpoint) {
			normal = checkpoint;
			checkpoint = Mathf.Floor(normal / checkpointStep + 1) * checkpointStep;
		}
		return normal;
	}

	// Linear interpolation for normalized level.
	// Invariant when adding or removing levels.
	// Example:  Editor/Tests/TestProgress.cs
	public int GetLevelNormal()
	{
		int levelRate = levelMax != 0 ? levelMax : 1;
		return levelNormalMax * level / levelRate;
	}

	public void SetLevelNormal(int level)
	{
		normal = level / (float)levelNormalMax;
	}
}
