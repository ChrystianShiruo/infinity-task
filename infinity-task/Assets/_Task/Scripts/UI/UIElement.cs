using UnityEngine;

namespace Task.UI.Elements
{
    public abstract class UIElement : MonoBehaviour
    {
        [SerializeField] protected RectTransform rectTransform;
        protected abstract void OnButtonClick();

        private void OnMouseUpAsButton()
        {
            OnButtonClick();
        }
    }
}