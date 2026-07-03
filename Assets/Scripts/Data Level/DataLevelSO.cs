using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public enum DataType
{
    False,
    Violation,
    Education,
    Entertainment,
    Polictic,
    Personal,
    None
}

[System.Serializable]
public class GroupSpawnConfig
{
    public string groupName;
    public GameObject groupPrefabs;
    [Range(0, 100)]
    public int change;
}

[System.Serializable]
public class DataSpawnConfig
{
    public string dataName;
    public DataType dataType;
    [Range(0, 100)]
    public int change;
}

[System.Serializable]
public class DataFlowLineData
{
    [Header("Data Flow Line Config")]
    public string dataFlowLineName;
    public DataFlowLineDirection dataFlowLineDirection;
    public DataFlowLineSpeed dataFlowLineSpeed;

    [Header("Data Flow Line Spawner Config")]
    //Spawn interval in seconds
    public float spawnIntervalMin;
    public float spawnIntervalMax;
    //Spawner groups change
    public List<GroupSpawnConfig> groupSpawnConfigList = new();
    //Data Spawn Change
    public List<DataSpawnConfig> dataSpawnConfigList = new();
}


[CreateAssetMenu(fileName = "DataLevelSO", menuName = "Data Levels/DataLevelSO")]
public class DataLevelSO : ScriptableObject
{
    public List<DataFlowLineData> dataFlowLineDataList = new ();
}
