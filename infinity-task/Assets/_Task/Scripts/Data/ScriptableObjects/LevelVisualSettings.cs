using UnityEngine;

namespace Task.Data.Visual
{
    [CreateAssetMenu(fileName = "LevelVisualSettings", menuName = "Data/Visual/LevelVisualSettings")]
    public class LevelVisualSettings : ScriptableObject
    {
        public Color on = Color.white;
        public Color off = Color.white;
        [Range(.1f, .6f)] public float widthOn = .2f;
        [Range(.1f, .6f)] public float widthOff = .1f;
    }
}
