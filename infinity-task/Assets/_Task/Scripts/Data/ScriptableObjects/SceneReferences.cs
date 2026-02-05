using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Task.Data
{
    [CreateAssetMenu(fileName = "SceneReferences", menuName = "Data/SceneReferences")]
    public class SceneReferences : ScriptableObject
    {
        public IReadOnlyList<SceneReference> OrderedLevelScenes => _orderedLevelScenes;

        public SceneReference mainScene;
        public SceneReference menuScene;
        public SceneReference loadingScene;

        [Tooltip("Levels sequence will follow this order")]
        [SerializeField] private SceneReference[] _orderedLevelScenes;

        public void ValidateData()
        {
#if UNITY_EDITOR
            if(_orderedLevelScenes == null) return;

            bool isDirty = false;
            bool gotDirty;

            for(int i = 0; i < _orderedLevelScenes.Length; i++)
            {
                ValidateSceneFields(_orderedLevelScenes[i], out gotDirty);
                if(gotDirty) isDirty = true;
            }

            ValidateSceneFields(mainScene, out gotDirty);
            if(gotDirty) isDirty = true;

            ValidateSceneFields(menuScene, out gotDirty);
            if(gotDirty) isDirty = true;

            ValidateSceneFields(loadingScene, out gotDirty);
            if(gotDirty) isDirty = true;

            if(isDirty)
            {
                EditorUtility.SetDirty(this);
            }
#endif
        }

        public int GetLevelIndex(int buildIndex)
        {
            if(_orderedLevelScenes == null) return -1;
            return Array.FindIndex(_orderedLevelScenes, (sceneRef) => sceneRef?.buildIndex == buildIndex);
        }

        private void OnValidate()
        {
#if UNITY_EDITOR
            EditorApplication.delayCall += ValidateData;
#endif
        }

        private void ValidateSceneFields(SceneReference scene, out bool isDirty)
        {
            isDirty = false;

#if UNITY_EDITOR
            if(scene == null || scene.sceneAsset == null) return;

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

                if(index == -1 && !string.IsNullOrEmpty(path))
                {
                    Debug.LogWarning($"Scene '{scene.sceneName}' is not in Build Settings! Index will be -1.");
                }
            }
#endif
        }
    }
}