using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ActiveTaskData
{
    public TaskData taskData;
    public int currentInterceptedDataCount;
    public bool isCompletion;
}

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance { get; private set; }

    [SerializeField] private List<ActiveTaskData> activeTasks = new();
    [SerializeField] private TaskUIController taskUIController;

    public TaskSO testTaksData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
        activeTasks.Clear();
        foreach (var task in taskData.taskDataList)
        {
            activeTasks.Add(new ActiveTaskData
            {
                taskData = task,
                currentInterceptedDataCount = 0
            });
        }
        taskUIController.InitializTaskBoard(activeTasks);
    }

    private void CheckTaskCompletion(ActiveTaskData activeTask)
    {
        if (activeTask.currentInterceptedDataCount >= activeTask.taskData.interceptedDataCount)
        {
            Debug.Log($"Task [{activeTask.taskData.taskName}] completed!");
            SoundEffectManager.Instance.PlaySoundEffect("Task Complete");

            activeTask.isCompletion = true;
            // Handle task completion logic here (e.g., reward the player, update UI, etc.)
        }

        CheckAllTaskCompletion();
    }

    private void CheckAllTaskCompletion()
    {
        bool allTaskDone = true;

        foreach (var task in activeTasks)
        {
            if (!task.isCompletion)
            {
                allTaskDone = false;
                break; // no need to keep checking once we've found one unfinished task
            }
        }

        if (allTaskDone)
            GameManager.Instance.PlayerWinning();
    }

    public void CheckDropFile(DataFile dataFile)
    {
        foreach (var activeTask in activeTasks)
        {
            if (activeTask.taskData.dataType == dataFile.DataType)
            {
                activeTask.currentInterceptedDataCount++;
                Debug.Log($"Task [{activeTask.taskData.taskName}] progress: {activeTask.currentInterceptedDataCount}/{activeTask.taskData.interceptedDataCount}");
                taskUIController.UpdateTaskBoard(activeTask);
                CheckTaskCompletion(activeTask);
                return;
            }
        }

        PlayerHealthManager.Instance.OnTakingDamage();
    }
}
