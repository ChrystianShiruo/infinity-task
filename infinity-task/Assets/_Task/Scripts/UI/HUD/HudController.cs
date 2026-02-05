using UnityEngine;
using TMPro;
using Task.Core;
using Task.Data;
using System;

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
        }
        private void OnDisable()
        {
            if(Loader.Instance != null)
            {
                Loader.Instance.OnSceneLoaded -= UpdateLabel;
            }
        }

        private void UpdateLabel(SceneReference reference)
        {
            _sceneLabel.SetText(reference.sceneName);
        }
    }
}