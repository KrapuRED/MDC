using UnityEngine;

public class DataFlowLine : MonoBehaviour
{
    [Header("Data Line Config")]
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private DataFlowLineDirection direction;
    [SerializeField] private DataFlowLineSpeed speed;

    [Header("Reference Object")]
    [SerializeField] private DataFlowLineDataSpawner dataFlowLineDataSpawner;

    [Header("Test Data Line")]
    [SerializeField] private Color right = Color.red;
    [SerializeField] private Color left = Color.yellow;

    public DataFlowLineSpeed SpeedFlowLine => speed;

    public void InitializeDataFlow(DataFlowLineData config)
    {
        direction = config.dataFlowLineDirection;
        speed     = config.dataFlowLineSpeed;

        //FLIP THE POSITION OF THE START AND END POINTS BASED ON THE DIRECTION
        if (direction == DataFlowLineDirection.Right)
        {
            SwapPosition();
        }

        dataFlowLineDataSpawner.IntilizeDataSpawner(config.spawnInterval ,config.groupSpawnConfigList, config.dataSpawnConfigList);
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

    private void SwapPosition()
    {
        Vector2 prevStartPos = startPoint.position;
        Vector2 prevEndPos   = endPoint.position;
        
        startPoint.position = prevEndPos;
        endPoint.position = prevStartPos;
    }
}
