using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class TaskData
{
    public string taskName;
    public int interceptedDataCount;
    public DataType dataType;
}

[CreateAssetMenu(fileName = "TaskSO", menuName = "Taks/TaskSO")]
public class TaskSO : ScriptableObject
{
    public string taskSOName;
    public List<TaskData> taskDataList = new();
}
