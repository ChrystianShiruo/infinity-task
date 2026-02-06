using System.Collections.Generic;
using UnityEngine;

using Task.InGame.Managers;
using Task.Data;
using System;

namespace Task.Level.Part
{
    public abstract class BaseLevelElement : MonoBehaviour
    {
        public List<Vector2Int> ConnectorPositions { get => connectorPositions; }
        public bool Powered { get => _powered; }

        protected LineRenderer[] lineRenderers;
        protected SpriteRenderer spriteRenderer;
        protected List<Vector2Int> connectorPositions;

        private bool _powered = false;
        private Collider2D _collider;

        public abstract List<Vector2Int> UpdateConnectorPositions();
        protected abstract bool OnTryElementInteraction();
        protected abstract void OnDenyElementInteraction();
        protected abstract void OnTogglePower(bool on);


        protected virtual void Awake()
        {
            lineRenderers = GetComponentsInChildren<LineRenderer>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();
        }
        private void OnEnable()
        {
            if(LevelManager.Instance != null)
            {
                LevelManager.Instance.LevelEvents.OnLevelCompleted += DisableInteraction;
            }
        }
        private void OnDisable()
        {
            if(LevelManager.Instance != null)
            {
                LevelManager.Instance.LevelEvents.OnLevelCompleted -= DisableInteraction;
            }
        }

        private void Start()
        {
            SetColor(LevelManager.Instance.LevelVisualData.off);
        }
        private void DisableInteraction(SceneReference _, int i)
        {
            if(_collider == null)
            {
                return;
            }
            _collider.enabled = false;

        }

        private void OnMouseDown()
        {
#if UNITY_EDITOR
            //Debug.Log($"{this} pressed");
#endif
            LevelManager.Instance.LevelEvents.OnPartClicked?.Invoke(this);
            if(OnTryElementInteraction())
            {
                LevelManager.Instance.LevelEvents.OnPartChanged?.Invoke(this);
            }
            else
            {
                OnDenyElementInteraction();
            }
        }

        public void TogglePower(bool on)
        {
            if(on == _powered)
            {
                return;
            }
            _powered = on;
            var color = on ? LevelManager.Instance.LevelVisualData.on : LevelManager.Instance.LevelVisualData.off;
            SetColor(color);

            OnTogglePower(on);
        }

        private void SetColor(Color color)
        {
            if(lineRenderers != null)
            {
                foreach(var lineRenderer in lineRenderers)
                {
                    lineRenderer.startColor = color;
                    lineRenderer.endColor = color;
                }
            }
            if(spriteRenderer != null)
            {
                spriteRenderer.color = color;
            }
        }

    }
}