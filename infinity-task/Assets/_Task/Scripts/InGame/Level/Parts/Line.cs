using UnityEngine;

namespace Task.Level.Part
{
    public class Line : BaseLevelElement
    {
        protected override void OnElementClicked()
        {
            transform.Rotate(90f * Vector3.forward);
        }
    }
}