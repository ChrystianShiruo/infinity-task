using System;

using Task.Core;
using Task.Data;
using Task.InGame.Managers;

using UnityEngine;

public class CameraFit : MonoBehaviour
{
    [SerializeField] private float TargetWidth = 10f;
    [SerializeField] public float MinSize = 5f;

    private Camera _cam;

    private void Awake()
    {
        _cam = GetComponent<Camera>();
    }
    private void OnEnable()
    {
        if(Loader.Instance != null)
        {
            Loader.Instance.OnSceneLoaded += FitCamera;
        }
    }
    private void OnDisable()
    {
        if(Loader.Instance != null)
        {
            Loader.Instance.OnSceneLoaded -= FitCamera;
        }
    }

#if UNITY_EDITOR
    private void Update()
    {
        FitCamera();
    }
#endif


    private void FitCamera(SceneReference reference)
    {
        if(Loader.Instance.SceneReferences.GetLevelIndex(reference.buildIndex) == -1)
        {
            return;
        }
        FitCamera();
    }
    private void FitCamera()
    {
        if(LevelManager.Instance.CurrentLevel == null)
        {
            return;
        }
        float screenRatio = (float)Screen.width / (float)Screen.height;

        float sizeForWidth = (TargetWidth / screenRatio) / 2f;

        _cam.orthographicSize = Mathf.Max(sizeForWidth, MinSize);
    }
}