using UnityEngine;
using System.Collections.Generic;

public class GroupData : MonoBehaviour
{
    [Header("Group Data Config")]
    [SerializeField] private List<Transform> dataSlots; // pre-placed positions, max 5 in the prefab
    [SerializeField] private DataFile dataItemPrefab;
    [SerializeField] private int maxDataCount;

    [Header("Reference Object")]
    [SerializeField] private GroupDataMover groupDataMover;

    [SerializeField] private List<DataFile> _spawnedData = new();
    private int dataCount;
    private bool isFilled = false;

    public bool IsFilled => isFilled;

    public void FillGroup(List<DataSpawnConfig> dataSpawnConfig, Transform endPoint, DataFlowLineSpeed speedMode)
    {
        maxDataCount = dataSlots.Count;
        dataCount = Mathf.Clamp(dataCount, 1, dataSlots.Count);

        for (int i = 0; i < maxDataCount; i++)
        {
            DataType type = GetRandomDataType(dataSpawnConfig);
            DataFile item = Instantiate(dataItemPrefab, dataSlots[i].position, Quaternion.identity, transform);
            item.SetDataType(type);
            _spawnedData.Add(item);
        }

        if (_spawnedData.Count >= maxDataCount)
        {
            Debug.Log("Data group is filled to max capacity.");
            groupDataMover.InitializeMover(endPoint, speedMode);
        }
    }

    private DataType GetRandomDataType(List<DataSpawnConfig> dataSpawnConfig)
    {
        int totalWeight = 0;
        foreach (var config in dataSpawnConfig)
            totalWeight += config.change;

        int randomValue = Random.Range(0, totalWeight);
        int cumulative = 0;

        for (int i = 0; i < dataSpawnConfig.Count; i++)
        {
            cumulative += dataSpawnConfig[i].change;
            if (randomValue < cumulative)
                return dataSpawnConfig[i].dataType;
        }
        return DataType.None;
    }
}
