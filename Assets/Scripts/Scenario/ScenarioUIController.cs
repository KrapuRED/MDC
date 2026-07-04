using TMPro;
using UnityEngine;

public class ScenarioUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text textScenario;

    [SerializeField] private CanvasGroup canvasGroup;

    public void ShowScenario(ScenarioDataSO data)
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        textScenario.text = data.ScenarioText;

        canvasGroup.alpha = 1;
    }

    public void HideScenario()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
