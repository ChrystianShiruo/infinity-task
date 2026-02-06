using System;
using System.Collections.Generic;

using Task.Core;
using Task.InGame.Managers;
using Task.UI.Elements;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Task.UI {
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private GameObject _levelButtonPrefab;
        [SerializeField] private Transform _levelsParent;

        private void Start()
        {
            //if(LevelManager.Instance != null)
            //{
            //    LevelManager.Instance.LoadLevel(0);
            //}
            SetupLevelList();
        }

        private void SetupLevelList()
        {
            for(int i = 0; i < Loader.Instance.SceneReferences.OrderedLevelScenes.Count; i++)
            {
                LevelButton lb = Instantiate(_levelButtonPrefab, _levelsParent).GetComponent<LevelButton>();
                lb.Initialize(Loader.Instance.SceneReferences.OrderedLevelScenes[i], i);
                
            }

        }
    }
}