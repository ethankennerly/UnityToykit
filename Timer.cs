using System;

namespace Finegamedesign.Utils
{
    [Serializable]
    public sealed class Timer
    {
        public Observable<float> normal = new Observable<float>();
        public float NormalInState { get; private set; }
        public float resetTime = 0.0f;
        public float time = 0.0f;
        public float min = 0.0f;
        public float max = 40.0f;

        public bool isEnabled = true;

        public Observable<string> State = new Observable<string>();
        public int StateIndex { get; private set; }

        [Serializable]
        public sealed class StateNormal
        {
            public string state;
            public float normalMin;
            private float normalMax;

            public StateNormal(float theNormalMin, string theState)
            {
                state = theState;
                normalMin = theNormalMin;
                normalMax = 1.0f;
            }

            // Side-effect: Sets each normal max.
            // Expects state normals were already sorted by normal min.
            public static int GetIndex(StateNormal[] stateNormals, float normal)
            {
                if (stateNormals.Length == 0)
                {
                    return -1;
                }
                if (normal <= stateNormals[0].normalMin)
                {
                    return 0;
                }
                int index;
                int last = stateNormals.Length - 1;
                int previousIndex = 0;
                for (index = 1; index <= last; ++index)
                {
                    stateNormals[previousIndex].normalMax = stateNormals[index].normalMin;
                    previousIndex = index;
                }
                for (index = 0; index <= last; ++index)
                {
                    StateNormal stateNormal = stateNormals[index];
                    if (normal >= stateNormal.normalMin
                        && normal < stateNormal.normalMax)
                    {
                        return index;
                    }
                }
                return last;
            }

            public static StateNormal Get(StateNormal[] stateNormals, float normal)
            {
                int index = GetIndex(stateNormals, normal);
                if (index < 0)
                {
                    return null;
                }
                return stateNormals[index];
            }

            public float GetNormalInState(float normal)
            {
                float inState = (normal - normalMin) / (normalMax - normalMin);
                if (inState < 0.0f)
                {
                    inState = 0.0f;
                }
                else if (inState > 1.0f)
                {
                    inState = 1.0f;
                }
                return inState;
            }
        }

        public StateNormal[] stateNormals = {
            new StateNormal(0.0f, "fast2"),
            new StateNormal(0.25f, "fast1"),
            new StateNormal(0.50f, "fast"),
            new StateNormal(0.75f, "slow")
        };

        public void Reset()
        {
            time = resetTime;
            normal.value = Normalize(time);
        }

        public void Update(float deltaTime)
        {
            if (!isEnabled)
            {
                return;
            }
            time += deltaTime;
            normal.value = Normalize(time);
        }

        public void Clamp()
        {
            if (time > max)
            {
                time = max;
            }
            if (time < min)
            {
                time = min;
            }
            normal.value = Normalize(time);
        }

        private float Normalize(float theTime)
        {
            float normal = (theTime - min) / (max - min);
            if (normal < min)
            {
                normal = min;
            }
            else if (normal > max)
            {
                normal = max;
            }
            StateIndex = StateNormal.GetIndex(stateNormals, normal);
            StateNormal stateNormal = stateNormals[StateIndex];
            State.value = stateNormal.state;
            NormalInState = stateNormal.GetNormalInState(normal);
            return normal;
        }
    }
}
