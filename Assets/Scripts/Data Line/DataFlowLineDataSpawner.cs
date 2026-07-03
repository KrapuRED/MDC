using UnityEngine;
using System.Collections.Generic;

public class DataFlowLineDataSpawner : MonoBehaviour
{
    [Header("Data Line Spawner Data Config")]
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;
    [SerializeField] private float spawnIntervalMin;
    [SerializeField] private float spawnIntervalMax;
    [SerializeField] private Transform spawnerContainer;

    [Header("Data Secure Spawner Config")]
    [SerializeField] private Transform lastSpawnGroup;
    [SerializeField] private float secureDistance = 6f;
    [SerializeField] private bool isSecureSpawn = true;

    private List<GroupSpawnConfig> _groupSpawnConfig = new ();
    private List<DataSpawnConfig> _dataSpawnConfig = new();
    private float currentSpawnInterval;
    private bool isIntilize;

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

    public void GenerateData()
    {
        if (!isSecureSpawn) 
        { 
            return; 
        }

        isSecureSpawn = false;

        // 1) Fill the group with data based on the group spawn config
        var groupPrefab = GetRandomGroupData();
        
        GameObject groupInstance = Instantiate(groupPrefab, startPosition.position, Quaternion.identity, spawnerContainer);
        string newName = $"{transform.parent.name} - {groupInstance.name}";
        groupInstance.name = newName;
        GroupData groupData = groupInstance.GetComponent<GroupData>();

        lastSpawnGroup = groupInstance.transform;

        groupData.FillGroup(_dataSpawnConfig, endPosition);
    }
}
