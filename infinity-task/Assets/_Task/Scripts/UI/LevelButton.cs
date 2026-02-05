
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Task.Core;
using Task.Data;
using Task.InGame.Managers;


namespace Task.UI.Elements
{
    public class LevelButton : UIElement
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _nameLabel;
        [SerializeField] private List<GameObject> _scoreVisuals;

        private SceneReference _levelReference;
        private LevelData _levelData;
        private int _levelOrder = -1;
        public void Initialize(SceneReference sceneReference, int levelOrder)
        {
            _levelReference = sceneReference;
            _levelOrder = levelOrder;
            _nameLabel.SetText(sceneReference.sceneName);
            _button.onClick.AddListener(OnButtonClick);
            _levelData = DataManager.PlayerData.GetLevelDataBybuildIndex(_levelReference.buildIndex);
            if(_levelData != null)
            {
                SetScoreUI();
            }
            SetInteractable();
        }

        

        protected override void OnButtonClick()
        {
            Amplitude.getInstance().logEvent("OnButtonClick");

            LevelManager.Instance.LoadLevel(_levelReference.buildIndex);
            StartCoroutine(Loader.Instance.UnloadMenu());
        }
        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }
        private void SetInteractable()//lock/unlock level button
        {
            _button.interactable = _levelOrder <= (DataManager.PlayerData.lastCompletedLevelIndex + 1);
        }

        private void SetScoreUI()
        {

            for(int i = 0; i < _scoreVisuals.Count; i++)
            {
                _scoreVisuals[i].SetActive(i < _levelData.score);
            }
        }
    }
}