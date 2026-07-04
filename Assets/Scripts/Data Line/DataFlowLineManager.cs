using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

[System.Serializable]
public enum DataFlowLineDirection
{
    none,
    left,
    Right
}

[System.Serializable]
public enum DataFlowLineSpeed
{
    SuperSlow,
    Slow,
    Normal,
    LittleFast,
    Fast
}

public class DataFlowLineManager : MonoBehaviour
{
    public static DataFlowLineManager instance { get; private set; }

    [SerializeField] private List<Transform> dataFlowLinePoints = new();

    [Header("TESTING PURPOSES ONLY")]
    public DataLevelSO dataLevelSO;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (dataLevelSO != null)
        {
            InitializeDataLine(dataLevelSO);
        }
    }

    public void InitializeDataLine(DataLevelSO dataLevel)
    {
        for (int i = 0; i < dataLevel.dataFlowLineDataList.Count; i++)
        {
            BuildDataFlowLines(dataFlowLinePoints[i], dataLevel.dataFlowLineDataList[i]);
        }
    }

    private void BuildDataFlowLines(Transform dataFlowLinePoint, DataFlowLineData config)
    {
        DataFlowLine dataFlowLine = dataFlowLinePoint.GetComponent<DataFlowLine>();
        if (dataFlowLine == null )
        {
            Debug.LogError($"{dataFlowLinePoint.name} is missing the script of DataFlowLine!");
            return;
        }

        dataFlowLine.InitializeDataFlow(config);
    }

    public void ApplySpeedModifier(DataFlowLineSpeed speedMode)
    {
        int randomIndex = Random.Range(0, dataFlowLinePoints.Count);

        DataFlowLine selectLine = dataFlowLinePoints[randomIndex].GetComponent<DataFlowLine>();
        selectLine.ChangeSpeedMode(speedMode);
    }
}
