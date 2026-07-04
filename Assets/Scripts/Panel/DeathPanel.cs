using UnityEngine;

public enum PanelActivationMode
{
    BelowThreshold,   // shows when GameManager.Instance.Level <  levelActive
    AtOrAboveThreshold // shows when GameManager.Instance.Level >= levelActive
}

public class DeathPanel : MonoBehaviour
{

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private PanelActivationMode activationMode;
    [SerializeField] private int levelActive;

    private void OnEnable()
    {
        GlobalEvent.OnShowDeathPanel.AddListener(ShowPanel);
    }

    private void OnDisable()
    {
        OnRemoveListener();
    }

    private void OnDestroy()
    {
        OnRemoveListener();
    }

    private void OnRemoveListener()
    {
        GlobalEvent.OnShowDeathPanel.RemoveListener(ShowPanel);
    }

    private void ShowPanel()
    {
        if (this == null)
            return;

        bool shouldShow = activationMode switch
        {
            PanelActivationMode.BelowThreshold => GameManager.Instance.Level < levelActive,
            PanelActivationMode.AtOrAboveThreshold => GameManager.Instance.Level >= levelActive,
            _ => false
        };

        if (!shouldShow)
            return;

        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnClickRetry()
    {
        GameManager.Instance.OnRetryGame();
    }

    public void OnClickExit()
    {
        GameManager.Instance.OnExitGame();
    }
}
