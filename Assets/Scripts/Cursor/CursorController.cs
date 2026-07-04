using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorController : MonoBehaviour
{
    
    [SerializeField] private bool isDragging = false;
    [SerializeField] private DataFile hoveredObject2D;
    [SerializeField] private DataFile draggedObject2D;
    private Vector2 dragOffset2D;

    [Header("Detection Config")]
    [SerializeField] private float grabRadius = 0.6f;

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
        Vector2 mouseWorldPos = GetMouseWorldPosition2D();
        Collider2D[] hits = Physics2D.OverlapCircleAll(mouseWorldPos, grabRadius);

        DataFile closestData = null;
        float closestDistance = grabRadius; // Batas jarak maksimum deteksi

        // 2. Cari objek dengan komponen DataFile yang posisinya paling dekat dengan kursor
        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent(out DataFile dataFile))
            {
                float distance = Vector2.Distance(mouseWorldPos, hit.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestData = dataFile;
                }
            }
        }

        // 3. Masukkan hasil objek terdekat ke hoveredObject2D
        hoveredObject2D = closestData;

        if (hoveredObject2D != null)
        {
            Debug.Log($"Hovering: {hoveredObject2D.name}");
        }
    }

    private void OnStartDrag()
    {
        if (hoveredObject2D == null) return;

        if (hoveredObject2D.TryGetComponent(out DataFile dataFile))
        {
            draggedObject2D = dataFile;
            draggedObject2D.OnDrag();
            isDragging = true;
        }
    }

    private void OnEndDrag()
    {
        if (draggedObject2D != null)
        {
            Vector2 finalDropPos = GetMouseWorldPosition2D();
            draggedObject2D.OnDrop(finalDropPos);
        }

        isDragging = false;
        draggedObject2D = null;
    }
}
