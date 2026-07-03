using UnityEngine;
using System.Collections.Generic;

public class DataFlowLineDataSpawner : MonoBehaviour
{
    [Header("Data Line Spawner Data Config")]
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;
    [SerializeField] private float spawnInterval = 1f;
    
    private List<GroupSpawnConfig> _groupSpawnConfig = new ();
    private List<DataSpawnConfig> _dataSpawnConfig = new();

    public void IntilizeDataSpawner(float spawnRate, List<GroupSpawnConfig> groupConfig, List<DataSpawnConfig> dataConfig)
    {
        if (groupConfig == null || dataConfig == null)
        {
            Debug.LogError("GroupSpawnConfig or DataSpawnConfig is null. Please provide valid configurations.");
            return;
        }

        spawnInterval = spawnRate;
        _groupSpawnConfig = groupConfig;
        _dataSpawnConfig = dataConfig;

        StartGenerateData();
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

    private DataType GetRandomDataType()
    {
        int totalWeight = 0;
        foreach (var config in _dataSpawnConfig)
            totalWeight += config.change;

        int randomValue = Random.Range(0, totalWeight);
        int cumulative = 0;

        for (int i = 0; i < _dataSpawnConfig.Count; i++)
        {
            cumulative += _dataSpawnConfig[i].change;
            if (randomValue < cumulative)
                return _dataSpawnConfig[i].dataType;
        }
        return DataType.None;
    }

    public void StartGenerateData()
    {
        var groupPrefab = GetRandomGroupData();
        var dataType    = GetRandomDataType();

        Debug.Log($"[{transform.parent.name} - StartGenerateData] groupPrefab: {groupPrefab}, dataType: {dataType}");
        // 1) Fill the group with data based on the group spawn config
        // 2) Initilize Group Mover
    }
}
