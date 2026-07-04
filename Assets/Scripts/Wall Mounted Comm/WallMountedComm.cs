using UnityEngine;

public class WallMountedComm : MonoBehaviour, IInteractable
{
    public void OnInteract()
    {
        Debug.Log($"Interact with {gameObject.name}");
        DialogueManager.Instance.TriggerDialogue();
    }
}
