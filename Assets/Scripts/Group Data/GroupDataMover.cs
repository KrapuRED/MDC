using UnityEngine;

public class GroupDataMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private Transform _endPosition;
    [SerializeField] private bool isInitialized = false;

    private void Update()
    {
        if (!isInitialized)
            return;

        MoveGroupData();
    }

    public void InitializeMover(Transform endPositions)
    {
        if (isInitialized)
        {
            Debug.LogWarning($"[{gameObject.name} - GroupDataMover] Is Been Initialized!.");
            return;
        }

        if (endPositions == null)
        {
            Debug.LogError($"[{gameObject.name} - GroupDataMover] End position is not assigned.");
            return;
        }

            _endPosition   = endPositions;

        isInitialized = true;
    }

    private void MoveGroupData()
    {
        if (_endPosition == null)
        {
            Debug.LogError($"[{gameObject.name} - GroupDataMover] Start or End position is not assigned.");
            return;
        }

        if (DistanceCheck())
            return;

        Debug.Log($"[{gameObject.name} - GroupDataMover] Moving group data...");
        
        transform.position =  Vector2.MoveTowards(transform.position, _endPosition.position, moveSpeed * Time.deltaTime);
    }

    private bool DistanceCheck()
    {
        if (Vector2.Distance(transform.position, _endPosition.position) < 0.1f)
        {
            Debug.Log($"[{gameObject.name} - GroupDataMover] Reached end position.");
            isInitialized = false;
            return true;
        }

        return false;
    }
}
