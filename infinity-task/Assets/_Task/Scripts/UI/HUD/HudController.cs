using UnityEngine;
using TMPro;
using Task.Core;
using Task.Data;
using System;
using Task.InGame.Managers;

namespace Task.UI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _sceneLabel;

        private void OnEnable()
        {
            if(Loader.Instance != null)
            {
                Loader.Instance.OnSceneLoaded += UpdateLabel;
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
                Loader.Instance.OnSceneLoaded -= UpdateLabel;
            }
            if(LevelManager.Instance != null)
            {
                LevelManager.Instance.LevelEvents.OnLevelCompleted -= LevelCompletedFeedback;
            }
        }

        private void LevelCompletedFeedback(SceneReference reference, int arg2)
        {
            //TODO: flashy stuff
            _sceneLabel.SetText("Amazing!!");
        }
        private void UpdateLabel(SceneReference reference)
        {
            _sceneLabel.SetText(reference.sceneName);
        }
    }
}