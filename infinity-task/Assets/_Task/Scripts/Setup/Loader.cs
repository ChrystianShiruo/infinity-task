using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Task.Data;
using Task.InGame.Managers;

namespace Task.Core
{
    public class Loader : MonoBehaviour
    {
        public SceneReferences SceneReferences { get => _sceneReferences; }

        [SerializeField] private SceneReferences _sceneReferences;


        public IEnumerator LoadScene(int index)
        {
            yield return SceneManager.LoadSceneAsync(_sceneReferences.loadingScene.buildIndex, LoadSceneMode.Additive);

            yield return SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);

            yield return UnloadScene(_sceneReferences.loadingScene.buildIndex);
        }
        public IEnumerator UnloadScene(int index)
        {
            yield return SceneManager.UnloadSceneAsync(index);
        }



        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
        private void Start()
        {
            //StartCoroutine(LoadScene(_levels.menuScene.buildIndex));
            //StartCoroutine(LoadScene(_sceneReferences.LevelScenes[0].buildIndex));
            LevelManager.Instance.LoadLevel(0);
        }

    }
}