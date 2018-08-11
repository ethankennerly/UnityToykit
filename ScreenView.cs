using UnityEngine/*<Screen>*/;

namespace FineGameDesign.Utils
{
    public sealed class ScreenView
    {
        public static bool IsPortrait()
        {
            return Screen.width < Screen.height;
        }

        // https://docs.unity3d.com/ScriptReference/ScreenOrientation.AutoRotation.html
        public static void AutoRotate()
        {
            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = true;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
            Screen.orientation = ScreenOrientation.AutoRotation;
        }
    }
}
