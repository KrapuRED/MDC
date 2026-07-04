using UnityEngine;

public class VisibilityCheck : MonoBehaviour
{
    [SerializeField] private GameObject objIndicator;
    private Camera mainCamera;

    private IIndicatorable _indicator;

    void Start()
    {
        mainCamera = Camera.main; // Cache the main camera
        _indicator = objIndicator.GetComponent<IIndicatorable>();
    }

    void Update()
    {
        Vector3 viewPos = mainCamera.WorldToViewportPoint(transform.position);

        // Check if the object's center is outside the 0 to 1 boundaries
        if (viewPos.x < 0f || viewPos.x > 1f || viewPos.y < 0f || viewPos.y > 1f)
        {
            Debug.Log("Object is out of camera boundaries!");
            _indicator.ShowIndicator();
        }
        else
            _indicator.HideIndicator();
    }
}
