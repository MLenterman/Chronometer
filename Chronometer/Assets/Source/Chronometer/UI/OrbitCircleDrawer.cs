using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chronometer
{

    public class OrbitCircleDrawer : MonoBehaviour
    {
        public float ThetaScale = 0.01f;
        public float Radius = 1f;
        public float LineWidth = 1f;

        [ColorUsageAttribute(true, true)]
        public Color Color = Color.white;

        private LineRenderer lineRenderer;

        public void Awake()
        {
            lineRenderer = gameObject.GetComponent<LineRenderer>();
            lineRenderer.material.SetColor("_UnlitColor", Color);
        }

        public void Draw()
        {
            int size = (int)((1f / ThetaScale) + 1f);
            lineRenderer.positionCount = size;

            lineRenderer.startWidth = LineWidth;
            lineRenderer.endWidth = LineWidth;

            float theta = 0f;

            for (int i = 0; i < size; i++)
            {
                theta += (2.0f * Mathf.PI * ThetaScale);
                float x = Radius * Mathf.Cos(theta);
                float z = Radius * Mathf.Sin(theta);
                lineRenderer.SetPosition(i, new Vector3(x, 0f, z));
            }
        }
    }
}
