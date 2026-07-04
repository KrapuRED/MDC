using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorController : MonoBehaviour
{
    [SerializeField] private bool isDragging = false;
    [SerializeField] private Collider2D hoveredObject2D;
    [SerializeField] private Collider2D draggedObject2D;
    private Vector2 dragOffset2D;

    private GamePlayInput _gamePlayInput;
    private Camera mainCam;

    private void Awake()
    {
        _gamePlayInput = new GamePlayInput();
        Cursor.lockState = CursorLockMode.None;

        mainCam = Camera.main;
    }

    private void OnEnable()
    {
        _gamePlayInput.Enable();
    }

    private void OnDisable()
    {
        _gamePlayInput.Disable();
    }

    private void Start()
    {
        _gamePlayInput.Mouse.Drag.started += _ => OnStartDrag();
        _gamePlayInput.Mouse.Drag.canceled += _ => OnEndDrag();
    }

    private void LateUpdate()
    {
        if (isDragging && draggedObject2D == null)
        {
            isDragging = false;
        }

        if (!isDragging)
            OnDetechObject();

        if (isDragging && draggedObject2D != null)
        {
            Vector2 mouseWorldPos = GetMouseWorldPosition2D();
            draggedObject2D.transform.position = mouseWorldPos + dragOffset2D;
        }
    }

    private Vector2 GetMouseWorldPosition2D()
    {
        Vector2 screenPos = _gamePlayInput.Mouse.Position.ReadValue<Vector2>();
        return mainCam.ScreenToWorldPoint(screenPos);
    }

    private void OnDetechObject()
    {
        Vector2 mouseWorldPos2D = GetMouseWorldPosition2D();
        Collider2D hit2D = Physics2D.OverlapPoint(mouseWorldPos2D);

        if (hit2D != null && hit2D.CompareTag("DataFile"))
        {
            Debug.Log($"{hit2D.name}");

            if (hit2D.TryGetComponent(out DataFile dataFile))
            {
                hoveredObject2D = hit2D;
            }
            else
            {
                hoveredObject2D = null; // Bukan DataFile, abaikan
            }
        }
        else
        {
            hoveredObject2D = null;
        }
    }

    private void OnStartDrag()
    {
        if(hoveredObject2D == null) return;

        draggedObject2D = hoveredObject2D;

        if (draggedObject2D.TryGetComponent(out IDragable dragable))
        {
            dragable.OnDrag();
        }

        isDragging = true;

        Vector2 mouseWorldPos = GetMouseWorldPosition2D();
        dragOffset2D = (Vector2)draggedObject2D.transform.position - mouseWorldPos;

    }

    private void OnEndDrag()
    {
        if (draggedObject2D != null && draggedObject2D.TryGetComponent(out IDragable dragable))
        {
            dragable.OnDrop();
        }

        isDragging = false;
        draggedObject2D = null;
    }
}
