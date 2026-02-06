using UnityEngine;
using TMPro;
using Task.Core;
using Task.Data;
using System;
using UnityEngine.UI;
using Task.InGame.Managers;
using UnityEngine.Events;

namespace Task.UI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _sceneLabel;
        [SerializeField] private Button _menuButton;


        private void Awake()
        {
            _menuButton.onClick.AddListener(ReturnToMenu);
        }

        

        private void OnEnable()
        {
            if(Loader.Instance != null)
            {
                Loader.Instance.OnSceneLoaded += UpdateHud;
            }
            if(LevelManager.Instance != null)
            {
                LevelManager.Instance.LevelEvents.OnLevelCompleted += LevelCompletedFeedback;
            }
        }

        private void OnDisable()
        {
            if(Loader.Instance != null)
            {
                Loader.Instance.OnSceneLoaded -= UpdateHud;
            }
            if(LevelManager.Instance != null)
            {
                LevelManager.Instance.LevelEvents.OnLevelCompleted -= LevelCompletedFeedback;
            }
        }

        private void ReturnToMenu()
        {
            StartCoroutine(Loader.Instance.LoadMenu());
        }
        private void LevelCompletedFeedback(SceneReference reference, int arg2)
        {
            //TODO: flashy stuff
            _sceneLabel.SetText("Amazing!!");
            _menuButton.interactable = false;
        }
        private void UpdateHud(SceneReference reference)
        {
            UpdateLabel(reference);
            ShowMenuButton(reference);
        }

        private void ShowMenuButton(SceneReference reference)
        {
            bool show = (Loader.Instance.SceneReferences.GetLevelIndex(reference.buildIndex) != -1);
            _menuButton.gameObject.SetActive(show);
                
            _menuButton.interactable = show;

        }

        private void UpdateLabel(SceneReference reference)
        {
            _sceneLabel.SetText(reference.sceneName);
        }
    }
}