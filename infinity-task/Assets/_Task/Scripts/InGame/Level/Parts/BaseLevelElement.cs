using System;

using Task.InGame;

using UnityEngine;

namespace Task.Level.Part
{
    public abstract class BaseLevelElement : MonoBehaviour
    {
        protected abstract void OnElementClicked();

        protected LineRenderer lineRenderer;
        protected SpriteRenderer spriteRenderer;
        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
            SetColor(LevelManager.Instance.ColorData.off);
        }

        private void OnMouseDown()
        {
            Debug.Log($"{this} pressed");
            //TODO: trigger events; trigger feedback
            OnElementClicked();
            //TODO: evaluate
        }

        private void SetColor(Color color)
        {
            if(lineRenderer != null)
            {
                lineRenderer.startColor = color;
                lineRenderer.endColor = color;
            }
            else if(spriteRenderer != null)
            {
                spriteRenderer.color = color;
            }

        }
    }
}