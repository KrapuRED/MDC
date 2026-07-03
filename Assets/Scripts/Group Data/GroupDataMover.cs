using UnityEngine;

public class GroupDataMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;

    [SerializeField] private bool isInitialized = false;

    private void Update()
    {
        if (!isInitialized)
            return;

        MoveGroupData();
    }

    public void InitializeMover()
    {
        isInitialized = true;
    }

    private void MoveGroupData()
    {
        if (startPosition == null || endPosition == null)
        {
            Debug.LogError($"[{gameObject.name} - GroupDataMover] Start or End position is not assigned.");
            return;
        }

        if (DistanceCheck())
            return;

        Debug.Log($"[{gameObject.name} - GroupDataMover] Moving group data...");
        
        transform.position =  Vector2.MoveTowards(transform.position, endPosition.position, moveSpeed * Time.deltaTime);
    }

    private bool DistanceCheck()
    {
        if (Vector2.Distance(transform.position, endPosition.position) < 0.1f)
        {
            Debug.Log($"[{gameObject.name} - GroupDataMover] Reached end position.");
            isInitialized = false;
            return true;
        }

        return false;
    }
}
