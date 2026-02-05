using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Task.Data;
using Task.InGame.Managers;
using System;
using Task.InGame;

namespace Task.Core
{
    public class Loader : MonoBehaviour
    {
        public static Loader Instance { get; private set; }
        public SceneReferences SceneReferences { get => _sceneReferences; }

        public Action<SceneReference> OnSceneLoaded;
        public Action<SceneReference> OnSceneUnloaded;

        [SerializeField] private SceneReferences _sceneReferences;



        public IEnumerator LoadScene(SceneReference sceneReference)
        {
            yield return SceneManager.LoadSceneAsync(_sceneReferences.loadingScene.buildIndex, LoadSceneMode.Additive);

            yield return SceneManager.LoadSceneAsync(sceneReference.buildIndex, LoadSceneMode.Additive);
            OnSceneLoaded?.Invoke(sceneReference);


            yield return UnloadScene(_sceneReferences.loadingScene);

        }
        public IEnumerator UnloadScene(SceneReference sceneReference)
        {
            yield return SceneManager.UnloadSceneAsync(sceneReference.buildIndex);
            OnSceneUnloaded?.Invoke(sceneReference);
        }

        public IEnumerator LoadMenu()
        {
            yield return LoadScene(SceneReferences.menuScene);
        }
        public IEnumerator UnloadMenu()
        {
            yield return UnloadScene(SceneReferences.menuScene);
        }

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(this);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            StartCoroutine(LoadMenu());
        }

    }
}