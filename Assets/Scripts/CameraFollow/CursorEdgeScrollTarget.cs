using UnityEngine;

public class CursorEdgeScrollTarget : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera cam;

    [Header("Edge Scroll")]
    [Tooltip("How much of each screen edge triggers scrolling.")]
    [Range(0.05f, 0.4f)]
    [SerializeField] private float edgeSize = 0.18f;
    [Tooltip("Maximum look-ahead distance on the X axis.")]
    [SerializeField] private float maxOffsetX = 5f;
    [Tooltip("Maximum look-ahead distance on the Y axis.")]
    [SerializeField] private float maxOffsetY = 5f;

    [Header("Smoothing")]
    [Tooltip("Lower = snappier, Higher = smoother")]
    [SerializeField] private float smoothTime = 0.08f;

    private GamePlayInput input;
    private float velocityX;
    private float velocityY;

    // Fixed anchors - captured once, never recalculated from the camera's live position.
    // This is what stops the offset from compounding/drifting every frame.
    private float originX;
    private float originY;

    private void Awake()
    {
        input = new GamePlayInput();
        if (cam == null)
            cam = Camera.main;

        originX = transform.position.x;
        originY = transform.position.y;
    }

    private void OnEnable() => input.Enable();
    private void OnDisable() => input.Disable();

    private void LateUpdate()
    {
        Vector2 mouse = input.Mouse.Position.ReadValue<Vector2>();
        float viewportX = mouse.x / Screen.width;
        float viewportY = mouse.y / Screen.height;

        float desiredOffsetX = CalculateAxisOffset(viewportX, maxOffsetX);
        float desiredOffsetY = CalculateAxisOffset(viewportY, maxOffsetY);

        float targetX = originX + desiredOffsetX;
        float targetY = originY + desiredOffsetY;

        Vector3 pos = transform.position;
        pos.x = Mathf.SmoothDamp(pos.x, targetX, ref velocityX, smoothTime);
        pos.y = Mathf.SmoothDamp(pos.y, targetY, ref velocityY, smoothTime);
        transform.position = pos;
    }

    // Shared logic for both axes: viewport value 0..1, edge zones at each end,
    // returns a signed offset scaled by how deep into the edge zone the cursor is.
    private float CalculateAxisOffset(float viewportValue, float maxOffset)
    {
        if (viewportValue > 1f - edgeSize)
        {
            float t = Mathf.InverseLerp(1f - edgeSize, 1f, viewportValue);
            return t * maxOffset;
        }
        else if (viewportValue < edgeSize)
        {
            float t = Mathf.InverseLerp(edgeSize, 0f, viewportValue);
            return -t * maxOffset;
        }
        return 0f;
    }
}
