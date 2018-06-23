using System.Diagnostics;
using UnityEngine;

namespace Finegamedesign.Utils
{
    /// <summary>
    /// Wrapper of logging.
    /// Conditionally compiles if in debug mode or editor.
    /// </summary>
    public sealed class DebugUtil
    {
        [Conditional("DEBUG")]
        [Conditional("UNITY_EDITOR")]
        public static void Log(string message)
        {
            UnityEngine.Debug.Log(message);
        }

        [Conditional("DEBUG")]
        [Conditional("UNITY_EDITOR")]
        public static void LogWarning(string message)
        {
            UnityEngine.Debug.LogWarning(message);
        }

        [Conditional("DEBUG")]
        [Conditional("UNITY_EDITOR")]
        public static void Assert(bool condition, string message = "")
        {
            UnityEngine.Debug.Assert(condition, message);
        }
    }
}
