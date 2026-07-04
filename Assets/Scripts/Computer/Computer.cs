using UnityEngine;

public class Computer : MonoBehaviour, IInteractable
{
    public void OnInteract()
    {
       Debug.Log($"Interact with {gameObject.name}");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
