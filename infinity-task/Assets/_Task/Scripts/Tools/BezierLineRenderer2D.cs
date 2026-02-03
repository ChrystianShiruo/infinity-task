using UnityEngine;


namespace Task.Tools
{
    /// <summary>
    /// Helper tool for gerenating bezier curves with line renderer on the editor. Does not run at runtime.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(LineRenderer))]
    public class BezierLineRenderer2D : MonoBehaviour
    {

        [Header("Bezier Control Points (local space)")]
        public Vector2 startPoint = new Vector2(-2, 0);
        public Vector2 controlPoint1 = new Vector2(-1, 2);
        public Vector2 controlPoint2 = new Vector2(1, 2);
        public Vector2 endPoint = new Vector2(2, 0);


        [Header("Sampling")]
        [Range(8, 300)]
        public int segments = 60;


        private LineRenderer _line;

#if UNITY_EDITOR

        private void OnValidate()
        {
            if(!_line)
            {
                _line = GetComponent<LineRenderer>();
            }

            _line.useWorldSpace = false;
            Rebuild();
        }
        private void OnDrawGizmosSelected()
        {
            Matrix4x4 oldMatrix = Gizmos.matrix;

            Gizmos.matrix = transform.localToWorldMatrix;

            Gizmos.color = Color.gray;
            Gizmos.DrawLine(startPoint, controlPoint1);
            Gizmos.DrawLine(endPoint, controlPoint2);

            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(startPoint, 0.05f);
            Gizmos.DrawSphere(endPoint, 0.05f);

            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(controlPoint1, 0.05f);
            Gizmos.DrawSphere(controlPoint2, 0.05f);

            Gizmos.matrix = oldMatrix;
        }

        private void Rebuild()
        {
            if(segments <= 0)
            {
                return;
            }

            _line.positionCount = segments + 1;

            for(int i = 0; i <= segments; i++)
            {
                float t = i / (float)segments;
                Vector2 p = EvaluateBezier(t);
                _line.SetPosition(i, p);
            }
        }


        private Vector2 EvaluateBezier(float t)
        {
            float u = 1f - t;

            return
                u * u * u * startPoint +
                3f * u * u * t * controlPoint1 +
                3f * u * t * t * controlPoint2 +
                t * t * t * endPoint;
        }
#endif

        private void Awake()
        {
            // Editor only component
            this.enabled = false;
        }

    }
}