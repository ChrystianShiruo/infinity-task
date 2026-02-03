using System.Collections;

using Task.Data;
using Task.Data.Visual;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Task.InGame
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;
        public LevelEvents LevelEvents { get => _levelEvents; }
        public ColorData ColorData { get => _colorData;}

        [SerializeField] private SceneReferences _levels;
        [SerializeField] private ColorData _colorData;

        private LevelEvents _levelEvents;


        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(this);
                return;
            }
            Instance = this;
        }
        private void Start()
        {
            StartCoroutine(LoadNewLevel(0));
        }


        private IEnumerator LoadNewLevel(int i)
        {
            Debug.Log(_levels.levelScenes[i]);
            yield return SceneManager.LoadSceneAsync(_levels.levelScenes[i].buildIndex, LoadSceneMode.Additive);
        }

    }
}