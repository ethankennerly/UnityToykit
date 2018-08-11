using System;
using System.Collections.Generic;
using UnityEngine;

namespace FineGameDesign.Utils
{
    public sealed class KeyInputSystem : ASingleton<KeyInputSystem>
    {
        public static bool isVerbose = false;
        public static string backspaceCharacter = "\b";
        public static string newlineCharacter = "\n";
        /// <summary>
        /// Technically, Windows is "\r\n".  Mac is "\r".  Unix is "\n".
        /// </summary>
        public static string windowsNewlineCharacter = "\r";

        public event Action<float, float> onKeyDownXY;
        public event Action<float, float> onKeyXY;

        public void Update()
        {
            UpdateKeyDownAxis();
            UpdateKeyAxis();
        }

        private void UpdateKeyDownAxis()
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

        private void UpdateKeyAxis()
        {
            Vector2 axis = new Vector2();
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                axis.x = -1.0f;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                axis.x = 1.0f;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                axis.y = -1.0f;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                axis.y = 1.0f;
            }
            if (onKeyXY == null || axis.sqrMagnitude == 0.0f)
            {
                return;
            }
            axis.Normalize();
            onKeyXY(axis.x, axis.y);
        }

        /// <summary>
        /// If just pressed this frame.
        /// <a href="http://docs.unity3d.com/Manual/ConventionalGameInput.html">Conventional Input</a>
        /// <a href="http://answers.unity3d.com/questions/762073/c-list-of-string-name-for-inputgetkeystring-name.html">Key names</a>
        /// </summary>
        public static bool IsDownNow(string keyName)
        {
            return Input.GetKeyDown(keyName);
        }

        /// <summary>
        /// Represents enter key by "\n", even on Windows.
        /// Represents delete key or backspace key by "\b".
        /// Unity 5.6 inserts backspace character already but not delete key, except on WebGL.
        /// </summary>
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

        /// <summary>
        /// Character type is more efficient, but string type is more compatible with most data.
        /// </summary>
        public static List<string> InputList()
        {
            return DataUtil.Split(InputString(), "");
        }
    }
}
