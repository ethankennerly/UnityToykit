using UnityEngine;

namespace Finegamedesign.Utils
{
    public sealed class InputView
    {
        public static Vector2 GetKeyDownAxis()
        {
            Vector2 axis = new Vector2();
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                axis.x = -1.0f;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                axis.x = 1.0f;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                axis.y = -1.0f;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                axis.y = 1.0f;
            }
            return axis;
        }
    }
}
