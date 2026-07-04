using UnityEngine;

public class DataFlowLine : MonoBehaviour
{
    [Header("Data Line Config")]
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private DataFlowLineDirection direction;
    [SerializeField] private DataFlowLineSpeed speedMode;

    [Header("Reference Object")]
    [SerializeField] private DataFlowLineDataSpawner dataFlowLineDataSpawner;
    [SerializeField] private DataFlowStaticLineDrawer dataFlowStaticLineDrawer;

    [Header("Test Data Line")]
    [SerializeField] private Color right = Color.red;
    [SerializeField] private Color left = Color.yellow;

    public DataFlowLineSpeed SpeedFlowLine => speedMode;
    private DataFlowLineSpeed? _pendingSpeedMode;
    private bool isInitialize;

    public void InitializeDataFlow(DataFlowLineData config)
    {
        direction = config.dataFlowLineDirection;
        speedMode     = config.dataFlowLineSpeed;

        //FLIP THE POSITION OF THE START AND END POINTS BASED ON THE DIRECTION
        if (direction == DataFlowLineDirection.Right)
        {
            SwapPosition();
        }

        dataFlowLineDataSpawner.IntilizeDataSpawner(config);
        isInitialize = true;

        if (_pendingSpeedMode.HasValue)
        {
            Debug.Log($"{gameObject.name} applying pending speed mode {_pendingSpeedMode.Value} after init");
            ChangeSpeedMode(_pendingSpeedMode.Value);
            _pendingSpeedMode = null;
        }

        Color lineColor = Color.white;
        if (direction == DataFlowLineDirection.Right)
            lineColor = right;
        else
            lineColor = left;

        dataFlowStaticLineDrawer.DrawDataFlowLine(startPoint, endPoint, lineColor);
    }

    private void SwapPosition()
    {
        Vector2 prevStartPos = startPoint.position;
        Vector2 prevEndPos   = endPoint.position;
        
        startPoint.position = prevEndPos;
        endPoint.position = prevStartPos;
    }

    public void ChangeSpeedMode(DataFlowLineSpeed speedModeChange)
    {
        if (!isInitialize)
        {
            Debug.LogWarning($"{gameObject.name} not initialized yet — queuing speed mode {speedModeChange} to apply after init");
            _pendingSpeedMode = speedModeChange;
            return;
        }

        Debug.Log($"{gameObject.name} changing speed mode {speedMode} -> {speedModeChange}");
        speedMode = speedModeChange;
    }

    private void OnDrawGizmos()
    {

        if (direction == DataFlowLineDirection.Right)
        {
            Gizmos.color = right;
        }
        else if (direction == DataFlowLineDirection.left)
        {
            Gizmos.color = left;
        }
        else
        {
            Gizmos.color = Color.white;
        }

        Gizmos.DrawLine(startPoint.position, endPoint.position);
    }
}
