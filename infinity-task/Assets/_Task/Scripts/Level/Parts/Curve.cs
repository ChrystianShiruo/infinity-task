using UnityEngine;

namespace Task.Level.Part
{
    public class Curve : BaseLevelElement
    {
        protected override void OnElementClicked()
        {
            transform.Rotate(90f * Vector3.forward);
        }
    }
}