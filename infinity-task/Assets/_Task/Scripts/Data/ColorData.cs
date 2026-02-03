using UnityEngine;

namespace Task.Data.Visual
{

    [CreateAssetMenu(fileName = "ColorPalette", menuName = "Data/Visual/ColorPalette", order = 0)]
    public class ColorData : ScriptableObject
    {
        public Color on = Color.white;
        public Color off = Color.white;
    }
}
