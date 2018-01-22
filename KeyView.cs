using System;
using System.Collections.Generic;
using UnityEngine;

namespace Finegamedesign.Utils
{
    public sealed class KeyView
    {
        public static bool isVerbose = false;
        public static string backspaceCharacter = "\b";
        public static string newlineCharacter = "\n";
        // Technically, Windows is "\r\n".  Mac is "\r".  Unix is "\n".
        public static string windowsNewlineCharacter = "\r";

        public static event Action<float, float> onKeyDownXY;

        private static float s_UpdateTime = -1.0f;

        public static void Update()
        {
            if (s_UpdateTime == Time.time)
            {
                return;
            }
            s_UpdateTime = Time.time;
            UpdateKeyDownAxis();
        }

        private static void UpdateKeyDownAxis()
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
            if (onKeyDownXY == null || axis.sqrMagnitude == 0.0f)
            {
                return;
            }
            axis.Normalize();
            onKeyDownXY(axis.x, axis.y);
        }

        // If just pressed this frame.
        // Key naming conventions:
        // http://docs.unity3d.com/Manual/ConventionalGameInput.html
        // List of key names:
        // http://answers.unity3d.com/questions/762073/c-list-of-string-name-for-inputgetkeystring-name.html
        public static bool IsDownNow(string keyName)
        {
            return Input.GetKeyDown(keyName);
        }

        // Represents enter key by "\n", even on Windows.
        // Represents delete key or backspace key by "\b".
        // Unity 5.6 inserts backspace character already but not delete key, except on WebGL.
        public static string InputString()
        {
            string input = Input.inputString;
            if (Input.GetKeyDown("delete"))
            {
                if (input.IndexOf(backspaceCharacter) < 0)
                {
                    input += backspaceCharacter;
                }
            }
            input = input.Replace(windowsNewlineCharacter + newlineCharacter, newlineCharacter)
                .Replace(windowsNewlineCharacter, newlineCharacter);
            input = input.Replace(backspaceCharacter + backspaceCharacter, backspaceCharacter);
            if (isVerbose && input != null && input != "")
            {
                DebugUtil.Log("KeyView.InputString: <" + input + ">"
                    + " length " + DataUtil.Length(input));
            }
            return input;
        }

        // Character type is more efficient, but string type is more compatible with most data.
        public static List<string> InputList()
        {
            return DataUtil.Split(InputString(), "");
        }
    }
}
