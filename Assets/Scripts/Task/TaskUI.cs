using UnityEngine;
using TMPro;

public class TaskUI : MonoBehaviour
{
    [SerializeField] private TMP_Text taskNameText;
    private ActiveTaskData _activeTaskData;

    public ActiveTaskData ActiveTaskData => _activeTaskData;

    public void UpdateTaskName(ActiveTaskData activeTaskData)
    {
        _activeTaskData = activeTaskData;
        taskNameText.text = $"{activeTaskData.taskData.taskName} {activeTaskData.currentInterceptedDataCount} / {activeTaskData.taskData.interceptedDataCount}";
    }
}
