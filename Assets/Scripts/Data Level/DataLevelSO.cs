using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class DataFlowLineData
{
    public string dataFlowLineName;
    public DataFlowLineDirection dataFlowLineDirection;
    public DataFlowLineSpeed dataFlowLineSpeed;
}


[CreateAssetMenu(fileName = "DataLevelSO", menuName = "Data Levels/DataLevelSO")]
public class DataLevelSO : ScriptableObject
{
    public List<DataFlowLineData> dataFlowLineDataList = new ();
}
