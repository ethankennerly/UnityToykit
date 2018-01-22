using UnityEngine/*<Mathf>*/;
using System.Collections.Generic/*<List>*/;

namespace Finegamedesign.Utils
{
    // Requirements:
    // 
    //     May go down in level.
    //     Do not repeat levels.  Once a level was repeated.
    // 
    // System:
    // 
    //     Adjust normalized level range.
    //     Deck of cards.
    //         Draw from deck at normalized position.
    //             Original index.
    //             Closest index in remaining.
    //         Identify original index.
    //
    // Test case:  2016-05-21 Anagram Attack.  Very high level.
    // Jennifer Russ expects to not feel overwhelmed.
    // Jennifer Russ expects to not repeat a word.
    public sealed class Progress
    {
        public int level = 0;
        public int levelNormalMax = 1000;
        public int levelMax = 0;
        public int levelUnlocked = 0;
        public float normal = 0.0f;
        public float radius = 
                0.03125f;
                // 0.0625f;
                // 0.1f;
        public bool isVerbose = false;

        public bool isCheckpoint = false;
        public float checkpointStep = -1.0f;
        public float checkpoint = -1.0f;

        private List<int> indexes = new List<int>();
        private int indexesLength = -1;
        private int indexesStart = 0;

        // Example:  Editor/Tests/TestProgress.cs
        public void SetupIndexes(int length, int start = 0)
        {
            indexesLength = length;
            indexesStart = start;
            DataUtil.Clear(indexes);
            for (int index = start; index < length; index++)
            {
                indexes.Add(index);
            }
            levelMax = length;
        }

        // Pop interpolated card from 0 to 1.
        // Example:  Editor/Tests/TestProgress.cs
        public int PopIndex()
        {
            if (DataUtil.Length(indexes) <= 0)
            {
                SetupIndexes(indexesLength, indexesStart);
            }
            int length = DataUtil.Length(indexes);
            int interpolated = (int)Mathf.Floor(normal * length);
            interpolated = Mathf.Min(length - 1, interpolated);
            int index = indexes[interpolated];
            indexes.RemoveAt(interpolated);
            level = index;
            return index;
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
            normal = ClampCheckpoint(normal);
            if (isVerbose) {
                DebugUtil.Log("Progress.creep: normal " + normal + " performance " + performanceNormal);
            }
            MayUnlock();
            return normal;
        }

        //
        // At each step of progress, clamp to checkpoint.
        // Example:  Editor/Tests/TestProgress.cs
        // 
        public void SetCheckpointStep(float step)
        {
            checkpointStep = step;
            if (0.0f <= step) {
                checkpoint = Mathf.Floor(normal / checkpointStep + 1) * checkpointStep;
                if (isVerbose) {
                    DebugUtil.Log("Progress.SetCheckpointStep: checkpoint " + checkpoint 
                        + " normal " + normal + " level " + GetLevelNormal());
                }
            }
            else {
                checkpoint = 0.0f;
            }
        }

        public float ClampCheckpoint(float normal)
        {
            isCheckpoint = 0.0f <= checkpoint && checkpoint <= normal;
            if (isCheckpoint) {
                normal = checkpoint;
            }
            return normal;
        }

        //
        // Discards excess progress after checkpoint.
        // Example:  Editor/Tests/TestProgress.cs
        // 
        public float UpdateCheckpoint(float normal = -1.0f)
        {
            if (normal == -1.0f) {
                normal = this.normal;
            }
            normal = ClampCheckpoint(normal);
            if (isCheckpoint) {
                normal = checkpoint;
                SetCheckpointStep(checkpointStep);
            }
            return normal;
        }

        // Linear interpolation for normalized level.
        // Invariant when adding or removing levels.
        // Example:  Editor/Tests/TestProgress.cs
        public int GetLevelNormal()
        {
            float levelRate = (float)(levelMax != 0 ? levelMax : 1.0f);
            level = (int)(Mathf.Ceil(normal * levelMax));
            return (int)(Mathf.Ceil(levelNormalMax * level / levelRate));
        }

        public void SetLevelNormal(int levelNormal)
        {
            normal = levelNormal / (float)levelNormalMax;
            if (0 == levelMax)
            {
                levelMax = levelNormalMax;
            }
            SetCheckpointStep(checkpointStep);
            MayUnlock();
        }

        // Example:  Editor/Tests/TestProgress.cs
        public void SetLevelNormalUnlocked(int levelNormal)
        {
            SetLevelNormal(levelNormal);
            levelUnlocked = (int)(normal * levelMax);
        }

        private void MayUnlock()
        {
            levelUnlocked = Mathf.Max(levelUnlocked, (int)(normal * levelMax));
        }
    }
}
