using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Task.Data
{
    [CreateAssetMenu(fileName = "SceneReferences", menuName = "Data/SceneReferences")]

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
                    if(scene.sceneName != scene.sceneAsset.name)
                    {
                        scene.sceneName = scene.sceneAsset.name;
                        isDirty = true;
                    }

                    string path = AssetDatabase.GetAssetPath(scene.sceneAsset);
                    int index = SceneUtility.GetBuildIndexByScenePath(path);

                    if(scene.buildIndex != index)
                    {
                        scene.buildIndex = index;
                        isDirty = true;

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