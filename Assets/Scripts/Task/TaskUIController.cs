using UnityEngine;
using System.Collections.Generic;
public class TaskUIController : MonoBehaviour
{
    [SerializeField] private Transform containerTaskBoard;
    [SerializeField] private List<TaskUI> taskUIList;
    [SerializeField] private GameObject perfabTaskUI;

    public void InitializTaskBoard(List<ActiveTaskData> activeTaskBoards)
    {
        foreach (Transform child in containerTaskBoard)
        {
            Destroy(child.gameObject);
        }
        taskUIList.Clear();


        Debug.Log($"TaskUIController: Initializing task board with {activeTaskBoards.Count} tasks.");
        foreach (var activeTaskData in activeTaskBoards)
        {
            GameObject newTaskUI = Instantiate(perfabTaskUI, containerTaskBoard);
            TaskUI taskUI = newTaskUI.GetComponent<TaskUI>();
            taskUI.UpdateTaskName(activeTaskData);
            taskUIList.Add(taskUI);
        }
    }

    public void UpdateTaskBoard(ActiveTaskData activeTaskBoard)
    {
        foreach (var taskUI in taskUIList)
        {
            if (taskUI.ActiveTaskData == activeTaskBoard && activeTaskBoard.currentInterceptedDataCount <= activeTaskBoard.taskData.interceptedDataCount)
            {
                taskUI.UpdateTaskName(activeTaskBoard);
                break;
            }
        }
    }
}
