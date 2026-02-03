using UnityEngine;

namespace Task.Level.Part
{
    public abstract class BaseLevelElement : MonoBehaviour
    {
        private void OnMouseDown()
        {
            Debug.Log($"{this} pressed");
            //TODO: trigger events; trigger feedback

            OnElementClicked();
            //TODO: evaluate
        }
        protected abstract void OnElementClicked();
    }
}