using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class DataFlowStaticLineDrawer : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float lineWidth;

    public void DrawDataFlowLine(Transform startPoint, Transform endPoint, Color lineColor)
    {
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.useWorldSpace = true;

        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;

        lineRenderer.positionCount = 2; // A single straight line needs 2 points
        lineRenderer.SetPosition(0, startPoint.position); // Index 0 is the start
        lineRenderer.SetPosition(1, endPoint.position);
    }
}
