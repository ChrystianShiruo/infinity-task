using UnityEngine;
using UnityEngine.SceneManagement; // Needed for SceneUtility
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Task.Data
{
    [CreateAssetMenu(fileName = "SceneReferences", menuName = "Data/SceneReferences", order = 1)]

    public class SceneReferences : ScriptableObject
    {
        public SceneReference[] levelScenes;

        public void ValidateData()
        {
#if UNITY_EDITOR
            bool isDirty = false;

            foreach(var scene in levelScenes)
            {
                if(scene != null && scene.sceneAsset != null)
                {
                    // 1. Update Name
                    if(scene.sceneName != scene.sceneAsset.name)
                    {
                        scene.sceneName = scene.sceneAsset.name;
                        isDirty = true;
                    }

                    // 2. Update Build Index
                    string path = AssetDatabase.GetAssetPath(scene.sceneAsset);
                    int index = SceneUtility.GetBuildIndexByScenePath(path);

                    if(scene.buildIndex != index)
                    {
                        scene.buildIndex = index;
                        isDirty = true;

                        // Optional: Warn if scene is missing from build settings
                        if(index == -1)
                        {
                            Debug.LogWarning($"Scene '{scene.sceneName}' is not in Build Settings! Index will be -1.");
                        }
                    }
                }
            }

            if(isDirty)
            {
                EditorUtility.SetDirty(this);
            }
#endif
        }

        private void OnValidate()
        {
            ValidateData();
        }
    }
}