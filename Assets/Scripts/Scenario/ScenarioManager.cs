using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    public static ScenarioManager Instance { get; private set; }

    [SerializeField] private ScenarioDataSO scenarioData;
    [SerializeField] private ScenarioUIController scenarioUIController;

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
        scenarioUIController.ShowScenario(scenarioData);
    }

    public void HideScenari()
    {
        scenarioUIController.HideScenario();
    }
}
