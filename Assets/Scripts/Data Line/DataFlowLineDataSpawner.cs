using UnityEngine;
using System.Collections.Generic;

public class DataFlowLineDataSpawner : MonoBehaviour
{
    [SerializeField] private DataFlowLine ownerDataFlowLine;

    [Header("Data Line Spawner Data Config")]
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;
    [SerializeField] private Transform spawnerContainer;
    [SerializeField] private float spawnIntervalMin;
    [SerializeField] private float spawnIntervalMax;

    [Header("Data Secure Spawner Config")]
    [SerializeField] private Transform lastSpawnGroup;
    [SerializeField] private float secureDistance = 6f;
    [SerializeField] private bool isSecureSpawn = true;

    private List<GroupSpawnConfig> _groupSpawnConfig = new ();
    private List<DataSpawnConfig> _dataSpawnConfig = new();
    private float currentSpawnInterval;
    private bool isIntilize;

    private void Start()
    {
        ownerDataFlowLine = GetComponentInParent<DataFlowLine>();
    }

    private void Update()
    {
        if (!isIntilize) return;

        CheckDistanceToLastGroup();

        if (currentSpawnInterval <= 0f && isSecureSpawn)
        {
            GenerateData();
            currentSpawnInterval = Random.Range(spawnIntervalMin, spawnIntervalMax);
        }
        else
        {
            currentSpawnInterval -= Time.deltaTime;
        }

    }

    private void CheckDistanceToLastGroup()
    {
        if (lastSpawnGroup == null)
        {
            isSecureSpawn = true;
            return;
        }

        float distance = Vector2.Distance(lastSpawnGroup.position, startPosition.position);
        if (distance > secureDistance)
        {
            isSecureSpawn = true;
        }
    }

    public void IntilizeDataSpawner(DataFlowLineData config)
    {
        if (config == null)
        {
            Debug.LogError("The Data Flow Line Config is null. Please provide valid configurations.");
            return;
        }

        spawnIntervalMin = config.spawnIntervalMin;
        spawnIntervalMax = config.spawnIntervalMax;

        _groupSpawnConfig = config.groupSpawnConfigList;
        _dataSpawnConfig = config.dataSpawnConfigList;

        isIntilize = true;
    }

    private GameObject GetRandomGroupData()
    {
        int cumulativeChance = 0;

        for (int i = 0; i < _groupSpawnConfig.Count; i++)
        {
            cumulativeChance += _groupSpawnConfig[i].change;
            int randomValue = Random.Range(0, 100);

            if (randomValue < cumulativeChance)
            {
                return _groupSpawnConfig[i].groupPrefabs;
            }
        }

        return null;
    }

    private void GenerateData()
    {
        if (!isSecureSpawn)
        {
            return;
        }

        // 1) Check if the prefab array/list returns a valid object
        var groupPrefab = GetRandomGroupData();
        if (groupPrefab == null)
        {
            Debug.LogWarning($"[{gameObject.name}] GenerateData failed: The group prefab is null! Check your GetRandomGroupData() logic or array configuration.");
            return;
        }

        // 2) Verify the spawn starting point
        if (startPosition == null)
        {
            Debug.LogError($"[{gameObject.name}] GenerateData failed: 'startPosition' is not assigned in the Inspector!");
            return;
        }

        // If all initial checks pass, lock the spawn state
        isSecureSpawn = false;

        // 3) Instantiate the object
        GameObject groupInstance = Instantiate(groupPrefab, startPosition.position, Quaternion.identity, spawnerContainer);
        if (groupInstance == null)
        {
            Debug.LogError($"[{gameObject.name}] GenerateData failed: Failed to instantiate {groupPrefab.name}!");
            return;
        }

        // 4) Safely check for a parent name to avoid exceptions if this object has no parent
        string parentName = transform.parent != null ? transform.parent.name : "NoParent";
        string newName = $"{parentName} - {groupInstance.name}";
        groupInstance.name = newName;

        // 5) Verify that the spawned instance actually contains the GroupData script
        GroupData groupData = groupInstance.GetComponent<GroupData>();
        if (groupData == null)
        {
            Debug.LogWarning($"[{gameObject.name}] GenerateData failed: Prefab '{groupInstance.name}' is missing the 'GroupData' component!");
            // Optional: Destroy the broken instance so it doesn't clutter your hierarchy
            Destroy(groupInstance);
            return;
        }

        lastSpawnGroup = groupInstance.transform;

        // 6) Verify the parameters needed before calling FillGroup
        if (_dataSpawnConfig == null)
        {
            Debug.LogError($"[{gameObject.name}] GenerateData warning: '_dataSpawnConfig' is null!");
        }
        if (endPosition == null)
        {
            Debug.LogError($"[{gameObject.name}] GenerateData warning: 'endPosition' is not assigned in the Inspector!");
        }

        DataFlowLineSpeed speedMode = DataFlowLineSpeed.Normal;
        if (ownerDataFlowLine == null)
        {
            Debug.LogWarning($"[{gameObject.name}] GenerateData warning: 'ownerDataFlowLine' is null! Falling back to default speedMode (0).");
        }
        else
        {
            speedMode = ownerDataFlowLine.SpeedFlowLine;
        }

        // Run the main logic with the validated parameters
        groupData.FillGroup(_dataSpawnConfig, endPosition, speedMode);
    }
}
