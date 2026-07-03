using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ActiveTaskData
{
    public TaskData taskData;
    public int currentInterceptedDataCount;
}

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance { get; private set; }

    [SerializeField] private List<ActiveTaskData> activeTasks = new();

    public TaskSO testTaksData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializTaskManager(testTaksData);
    }

    public void InitializTaskManager(TaskSO taskData)
    {
        foreach (var task in taskData.taskDataList)
        {
            activeTasks.Add(new ActiveTaskData
            {
                taskData = task,
                currentInterceptedDataCount = 0
            });
        }
    }

    private void CheckTaskCompletion(ActiveTaskData activeTask)
    {
        if (activeTask.currentInterceptedDataCount >= activeTask.taskData.interceptedDataCount)
        {
            Debug.Log($"Task [{activeTask.taskData.taskName}] completed!");
            // Handle task completion logic here (e.g., reward the player, update UI, etc.)
        }
    }

    public void CheckDropFile(DataFile dataFile)
    {
        foreach (var activeTask in activeTasks)
        {
            if (activeTask.taskData.dataType == dataFile.DataType)
            {
                activeTask.currentInterceptedDataCount++;
                Debug.Log($"Task [{activeTask.taskData.taskName}] progress: {activeTask.currentInterceptedDataCount}/{activeTask.taskData.interceptedDataCount}");
                CheckTaskCompletion(activeTask);
                return;
            }
        }
    }
}
