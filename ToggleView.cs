using UnityEngine;
using UnityEngine.UI;

namespace FineGameDesign.Utils
{
    public sealed class ToggleView
    {
        public static bool IsOn(GameObject button)
        {
            return button.GetComponent<Toggle>().isOn;
        }

        public static void SetIsOn(GameObject button, bool isOn)
        {
            button.GetComponent<Toggle>().isOn = isOn;
        }
    }
}
