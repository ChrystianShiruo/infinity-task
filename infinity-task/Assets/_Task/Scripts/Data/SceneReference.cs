using System;

namespace Task.Data
{
    [Serializable]
    public class SceneReference
    {
#if UNITY_EDITOR
        public UnityEditor.SceneAsset sceneAsset;
#endif

        public string sceneName;
        public int buildIndex = -1;
    }
}
