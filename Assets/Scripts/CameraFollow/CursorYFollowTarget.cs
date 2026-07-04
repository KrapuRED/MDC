using UnityEngine;

public class CursorYFollowTarget : MonoBehaviour
{
    [Header("Range")]
    [SerializeField] private float maxOffsetY = 5f; // clamp how far up/down this target can travel

    private float _originY;

    private GamePlayInput _gamePlayInput;

    private void Awake()
    {
        _gamePlayInput = new GamePlayInput();
        _originY = transform.position.y;
    }

    private void OnEnable()
    {
        _gamePlayInput.Enable();
    }

    private void OnDisable()
    {
        _gamePlayInput.Disable();
    }

    private void FixedUpdate()
    {
        Vector2 screenPos = _gamePlayInput.Mouse.Position.ReadValue<Vector2>();
        float normalizedY = (screenPos.y / Screen.height - 0.5f) * 2f;

        Vector3 pos = transform.position;
        pos.y = _originY + normalizedY * maxOffsetY;
        transform.position = pos;
    }
}
