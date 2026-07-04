using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    public static ScenarioManager Instance { get; private set; }

    [SerializeField] private ScenarioDataSO scenarioData;
    [SerializeField] private ScenarioUIController scenarioUIController;

    private bool isDoneRead;
    public bool IsDoneRead => isDoneRead;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        ShowScenario();
    }

    public void ShowScenario()
    {
        Debug.Log("ShowScenario");
        if (scenarioData == null)
        {
            HideScenari();
            return;
        }

        scenarioUIController.ShowScenario(scenarioData);
    }

    public void HideScenari()
    {
        isDoneRead = true;
        scenarioUIController.HideScenario();
        DialogueManager.Instance.TriggerDialogue();
    }
}
