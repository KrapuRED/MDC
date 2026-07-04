using UnityEngine;

public class Computer : MonoBehaviour, IInteractable
{
    public GameObject monitor;
    public GameObject highlightMonitor;

    private bool isComputerON;

    public void OnInteract()
    {
        if (!isComputerON)
            return;

        GameManager.Instance.OnPlayGame();
        Debug.Log($"Interact with {gameObject.name}");
    }

    public void TurnOnComputer()
    {
        isComputerON = true;
        monitor.SetActive(true);
    }

    public void OnHighLightComputer()
    {
        highlightMonitor.SetActive(true);
    }

    public void OffHighLightComputer()
    {
        highlightMonitor.SetActive(false);

    }
}
