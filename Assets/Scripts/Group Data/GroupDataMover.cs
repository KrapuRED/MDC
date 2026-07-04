using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SpeedModifier
{
    public string speedModifierName;
    public DataFlowLineSpeed speedMode;
    public float speedMultiplier;
}

public class GroupDataMover : MonoBehaviour
{
    [SerializeField] private List<SpeedModifier> speedModifiers = new List<SpeedModifier>();
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float currentSpeed;
    private DataFlowLineSpeed currentSpeedMode;

    [SerializeField] private Transform _endPosition;
    [SerializeField] private bool isInitialized = false;

    private void Update()
    {
        if (!isInitialized)
            return;

        MoveGroupData();
    }


    public void InitializeMover(Transform endPositions, DataFlowLineSpeed speedMode)
    {
        //Debug.Log($"[{gameObject.name} - GroupDataMover] Initialize Mover with speedMode mode: {speedMode}.");

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

        currentSpeedMode = speedMode;
        _endPosition  = endPositions;

        isInitialized = true;
    }

    private float CalculateSpeedGroup(DataFlowLineSpeed speedMode)
    {
        foreach (var modifier in speedModifiers)
        {
            if (modifier.speedMode == speedMode)
            {
                return modifier.speedMultiplier;
            }
        }

        return 1;
    }

    private void MoveGroupData()
    {
        if (_endPosition == null)
        {
            Debug.LogWarning($"[{gameObject.name} - GroupDataMover] Start or End position is not assigned.");
            return;
        }

        if (DistanceCheck())
        {
            Destroy(gameObject);
            return;
        }


        float speedMultiplier = CalculateSpeedGroup(currentSpeedMode); // Replace with the actual speedMode mode you want to use
        currentSpeed = baseSpeed * speedMultiplier;

        transform.position =  Vector2.MoveTowards(transform.position, _endPosition.position, currentSpeed * Time.deltaTime);
    }

    private bool DistanceCheck()
    {
        if (Vector2.Distance(transform.position, _endPosition.position) < 0.1f)
        {
            isInitialized = false;
            return true;
        }

        return false;
    }
}
