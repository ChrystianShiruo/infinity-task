using System;

namespace Task.Data
{
    [Serializable]
    public class SceneReference
    {
        public UnityEditor.SceneAsset sceneAsset;
        public string sceneName;
        public int buildIndex = -1;
    }
}
