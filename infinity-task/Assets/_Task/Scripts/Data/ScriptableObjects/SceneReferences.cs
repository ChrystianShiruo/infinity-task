using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using UnityEditor.SearchService;



#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Task.Data
{
    [CreateAssetMenu(fileName = "SceneReferences", menuName = "Data/SceneReferences")]

    public class SceneReferences : ScriptableObject
    {
        public IReadOnlyList<SceneReference> LevelScenes { get => _levelScenes; }

        public SceneReference mainScene;
        public SceneReference menuScene;
        public SceneReference loadingScene;

        [SerializeField] private SceneReference[] _levelScenes;

        public void ValidateData()
        {
#if UNITY_EDITOR
            bool isDirty = false;
            bool gotDirty;

            foreach(var scene in LevelScenes)
            {
                ValidateSceneFields(scene, out gotDirty);
                if(gotDirty)
                {
                    isDirty = true;
                }
            }
            ValidateSceneFields(mainScene, out gotDirty);
            if(gotDirty)
            {
                isDirty = true;
            }
            ValidateSceneFields(menuScene, out gotDirty);
            if(gotDirty)
            {
                isDirty = true;
            }
            ValidateSceneFields(loadingScene, out gotDirty);
            if(gotDirty)
            {
                isDirty = true;
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

        private void ValidateSceneFields(SceneReference scene, out bool isDirty)
        {
            isDirty = false;

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
    }
}