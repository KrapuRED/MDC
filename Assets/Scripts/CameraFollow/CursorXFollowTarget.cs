using UnityEngine;

public class CursorXFollowTarget : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera cam;

    [Header("Edge Scroll")]
    [Tooltip("How much of the top/bottom of the screen triggers scrolling.")]
    [Range(0.05f, 0.4f)]
    [SerializeField] private float edgeSize = 0.18f;
    [Tooltip("Maximum look-ahead distance.")]
    [SerializeField] private float maxOffset = 5f;

    [Header("Smoothing")]
    [Tooltip("Lower = snappier, Higher = smoother")]
    [SerializeField] private float smoothTime = 0.08f;

    private GamePlayInput input;
    private float velocityY;
    private float originY; // fixed anchor - captured once, never recalculated from live camera position

    private void Awake()
    {
        input = new GamePlayInput();
        if (cam == null)
            cam = Camera.main;

        originY = transform.position.y; // capture the target's starting Y as the stable anchor
    }

    private void OnEnable() => input.Enable();
    private void OnDisable() => input.Disable();

    private void LateUpdate()
    {
        Vector2 mouse = input.Mouse.Position.ReadValue<Vector2>();
        float viewportY = mouse.y / Screen.height;
        float desiredOffset = 0f;

        if (viewportY > 1f - edgeSize)
        {
            float t = Mathf.InverseLerp(1f - edgeSize, 1f, viewportY);
            desiredOffset = t * maxOffset;
        }
        else if (viewportY < edgeSize)
        {
            float t = Mathf.InverseLerp(edgeSize, 0f, viewportY);
            desiredOffset = -t * maxOffset;
        }

        // Anchored to a FIXED origin, not the camera's live position - bounded, no drift.
        float targetY = originY + desiredOffset;

        Vector3 pos = transform.position;
        pos.y = Mathf.SmoothDamp(pos.y, targetY, ref velocityY, smoothTime);
        transform.position = pos;
    }
}
